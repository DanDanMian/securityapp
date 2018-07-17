var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var UserSchema = new Schema({
  username: String,
  password: String,
  displayname: String,
  emailaddress: String,
  verifyCode: String,
  verifyStatus: Boolean
});

var User = module.exports = 
mongoose.model('User', UserSchema);