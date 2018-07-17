package com.yesprojectsteam.audiocall;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.telecom.PhoneAccount;
import android.telecom.PhoneAccountHandle;
import android.telecom.TelecomManager;
import android.util.Log;

import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    private final static String TAG = "MainActivity";
    static public String ggg;
    static public String fff;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_main);
        //registerAccount();
        //startCountdown(10);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent intent){
        Uri videoURI = intent.getData();
        fff = intent.getDataString();
        ggg = getPath(videoURI);
        //Log.i("This is uri",fff + "!!!!!!!!!!!!!!!!!!!!!!!!!!1");
        //Log.i("This is actual path", ggg + "!!!!!!!!!!!!!!!!!!!77777777777");
    }

    public void registerAccount() {
        TelecomManager tm = (TelecomManager) getSystemService(Context.TELECOM_SERVICE);
        PhoneAccountHandle phoneAccountHandle = new PhoneAccountHandle(
                new ComponentName(getApplicationContext(),
                        VoiceCall.class), "Security App");
        PhoneAccount phoneAccount =
                PhoneAccount.builder(phoneAccountHandle, "Security App")
                        .setCapabilities(PhoneAccount.CAPABILITY_CALL_PROVIDER)
                        .build();
        tm.registerPhoneAccount(phoneAccount);

        if (Build.MANUFACTURER.equalsIgnoreCase("Samsung")) {
            Intent intent = new Intent();
            intent.setComponent(new
                    ComponentName("com.android.server.telecom",
                    "com.android.server.telecom.settings.EnableAccountPreferenceActivity"));
            startActivity(intent);
        } else {
            //startActivity(new Intent(TelecomManager.ACTION_CHANGE_PHONE_ACCOUNTS));
            Intent newin = new Intent(TelecomManager.ACTION_CHANGE_PHONE_ACCOUNTS);
            newin.putExtra(TelecomManager.EXTRA_PHONE_ACCOUNT_HANDLE, phoneAccountHandle);
            startActivity(newin);
        }
    }

    public void startCountdown(int timer) {
        Intent countdown = new Intent(this, BroadcastService.class);
        countdown.putExtra("timer", timer);
        startService(countdown);
        Log.i(TAG, "Started service");
    }

    public void stopCountdown() {
        stopService(new Intent(this, BroadcastService.class));
        Log.i(TAG, "Stoped service");
    }

    ////////This is share function//////////////////////////////////////////////////////////////
    public void shareSetup(String msg){
        Intent sendIntent = new Intent();
        sendIntent.setAction(Intent.ACTION_SEND);
        sendIntent.putExtra(Intent.EXTRA_TEXT, msg);
        sendIntent.setType("text/plain");
        startActivity(sendIntent);
    }

    ///////////////////This is share function for specific app/////////////////////////////////////
    public void shareSetup(String msg, String app){
        Intent sendIntent = new Intent();
        sendIntent.setAction(Intent.ACTION_SEND);
        sendIntent.putExtra(Intent.EXTRA_TEXT, msg);
        sendIntent.setType("text/plain");
        switch (app) {
            case "facebook":
                sendIntent.setPackage("com.facebook.orca");
                break;
            case "whatsapp":
                sendIntent.setPackage("com.whatsapp");
                break;
            case "wechat":
                sendIntent.setPackage("com.tencent.mm");
                break;
        }
        try{
            startActivity(sendIntent);
        }
        catch (android.content.ActivityNotFoundException ex) {
            Log.i("unknown app", "Please install this app");
        }
    }

    ///////////////////////////////////This is video recording function//////////////////////////////////////////////////
    static final int REQUEST_VIDEO_CAPTURE = 1;
    static final int VIDEO_CAPTURE = 101;
    public void dispatchTakeVideoIntent() {
        Intent takeVideoIntent = new Intent(MediaStore.ACTION_VIDEO_CAPTURE);
        if (takeVideoIntent.resolveActivity(getPackageManager()) != null) {
            takeVideoIntent.putExtra(MediaStore.EXTRA_DURATION_LIMIT, 3);
            startActivityForResult(takeVideoIntent, REQUEST_VIDEO_CAPTURE);
        }
    }

    ///////////turn uri to actual path//////////////////////////////////////////////
    public String getPath(Uri uri) {
        String[] projection = { MediaStore.Images.Media.DATA };
        Cursor cursor = managedQuery(uri, projection, null, null, null);
        startManagingCursor(cursor);
        int column_index = cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }
}