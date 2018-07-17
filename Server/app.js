var express = require('express');
var mongoose = require('mongoose');
var bodyParser = require('body-parser');
var User = require('./user');
const { exec } = require('child_process');

mongoose.connect('mongodb://yesprojectsteam:kZBa2JLItJRA4gqK@securityapp-shard-00-00-jlfuh.mongodb.net:27017,securityapp-shard-00-01-jlfuh.mongodb.net:27017,securityapp-shard-00-02-jlfuh.mongodb.net:27017/test?ssl=true&replicaSet=SecurityApp-shard-0&authSource=admin');
var serverkey = 'AAAAOzCIsVQ:APA91bEmA03oN2A0gCpDSayfiNcpxlnFg5sthoMfzd1030B7e9XrdLxkOoP_zySjnGyD1g1V3ZV7OpVKV9yEGpleMMFapMClQqaK_0HYhxfIgEy515y0flbZhPdJZn7w5HWSUgeAfv_a';

var db = mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', function() {
});

var app = express();
var port = process.env.PORT || 8080;

// for parsing application/json
app.use(bodyParser.json());
// for parsing application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true })); 

//app.use(express.static(__dirname + '/root'));

function RegisterUser(req, res) {
	var user = new User({
		username: req.body.username,
		password: req.body.password,
		displayname: req.body.displayname,
		emailaddress: req.body.emailaddress,
		verifyCode: req.body.verifyCode,
		verifyStatus: false
	});

	user.save(function(err, user) {
		console.log('User Saved');
		if (err) {
			console.error(err);
			res.status(400).send('Register Unsuccessfully');
		}
		else if (!user) {
			console.error('User Did Not Create');
			res.status(400).send('Register Unsuccessfully');
		}
		else {
			console.log('User Registered: ' + req.body.username);
			res.status(200).send('Register Successfully');
		}
	});
}

app.post('/register', function(req, res){
	console.log('Register For User: ' + req.body.username);
	User.find({username: req.body.username}, function (err, users){
		console.log('Users Found');
		if(users.length > 0){
			user = users.pop();
			if(user.verifyStatus == true){
				console.error('Phone number registered');
				//res.status(400).send('Phone number registered');
				user.newPassword = req.body.password;
				user.verifyCode = req.body.verifyCode;
				user.save(function(err, user) {
					if (err) {
						console.error(err);
						res.status(400).send('New Password Unsuccessfully');
					}
					else if (!user) {
						console.error('User Did Not Create');
						res.status(400).send('New Password Unsuccessfully');
					}
					else {
						console.log('User Registered: ' + req.body.username);
						res.status(200).send('New Password Successfully');
					}
				});
			}
			else{
				User.remove({_id: user._id}, function (err){
					if (err) return handleError(err);
					RegisterUser(req, res);
				});
			}
		}
		else {
			RegisterUser(req, res);
		}
	});
});

app.post('/login', function(req, res){
	//console.log(req.body);
    var username = req.body.username;
	var password = req.body.password;
	User.find({username: username}, function (err, users) {
		user = users[0];
		if (err || user == undefined || user == null) {
			console.error(err);
			res.status(400).send('Wrong Username');
		}
		else if (user.password.toString() != password.toString()) {
			console.error('Wrong password');
			res.status(400).send('Wrong Password For User');
		}
		else if (user.verifyStatus == false){
			console.error('Not verified');
			res.status(400).send('User not verified. Please Sign up again.');
		}
		else {
			console.log('User Logined: ' + username);
			res.status(200).send(user.displayname);
		}
	});
});

app.post('/verify', function(req, res){
	var verifyCode = req.body.verifyCode;
	var username = req.body.username;
	User.find({username: username}, function (err, users){
		user = users.pop();
		if (err || user == undefined || user == null) {
			console.error(err);
			res.status(400).send('Wrong Username');
		}
		else if (user.verifyCode.toString() != verifyCode.toString()) {
			console.error('Wrong verify code');
			res.status(400).send('Wrong verify code For User');
		}
		else {
			console.log('User Logined: ' + username);
			user.verifyStatus = true;
			console.log('User verify status: '+ user.verifyStatus);

			if (user.newPassword && user.newPassword.length > 0) {
				user.password = user.newPassword;
				user.newPassword = '';
			}

			user.save(function(err, user) {
				if (err) {
					console.error(err);
					res.status(400).send('Verify Unsuccessfully');
				}
				else if (!user) {
					console.error('User Did Not Create');
					res.status(400).send('Verify Unsuccessfully');
				}
				else {
					console.log('User Registered: ' + username);
					res.status(200).send('Verify Successfully');
				}
			})
		}
	});
});

// Firebase notification management with API calls

var timeouts = new Map(); // string regtoken : Timeout countdown

app.post('/firebase', function(req, res) {
	var timedelay = req.body.timedelay;
	var regtoken = req.body.regtoken;
	console.log("Called Firebase Setup With " + timedelay + " Seconds");
	//var command = 'curl --header "Authorization: key='+serverkey+'" --header "Content-Type: application/json" https://android.googleapis.com/gcm/send -d "{\\"priority\\":\\"high\\",\\"notification\\":{\\"sound\\":\\"innovation.mp3\\",\\"title\\":\\"Ring Of Ruse\\",\\"body\\":\\"Incomming phone call\\"},\\"to\\":\\"'+regtoken+'\\"}"';


	var command = 'curl --header "Authorization: key='+serverkey+'" --header "Content-Type: application/json" https://fcm.googleapis.com/fcm/send -d "{\\"priority\\":\\"high\\",\\"notification\\":{\\"sound\\":\\"innovation.mp3\\",\\"title\\":\\"Ring Of Ruse\\",\\"body\\":\\"Incomming phone call\\"},\\"to\\":\\"'+regtoken+'\\"}"';

	var countdown = setTimeout(function() {
			timeouts.delete(regtoken);
			exec(command, function(err, stdout, stderr) {
			console.log(`stdout:\n ${stdout}`);
			console.log(`stderr:\n ${stderr}`);
			if (err) {
				console.error(err);
			}
		});
	}, timedelay * 1000);
	timeouts.set(regtoken, countdown);
	res.status(200).send('Firebase Setup Successfully');
});

app.post('/firebasecancel', function(req, res) {
	var regtoken = req.body.regtoken;
	console.log("Called Firebase Cancel: " + regtoken);
	var countdown = timeouts.get(regtoken);
	if (countdown == undefined || countdown == null) {
		console.error('Cannot find the timeout object');
		res.status(400).send('Cannot find the timeout object');
	}
	else {
		clearTimeout(countdown);
		console.log('Successfully cancelled firebase notification');
		res.status(200).send('Successfully cancelled firebase notification');
	}
});

app.get('/test', function(req, res){
	res.send('TEST TEST TEST');
});

app.listen(port, function() {
    console.log('Server running on port ' + port);
});