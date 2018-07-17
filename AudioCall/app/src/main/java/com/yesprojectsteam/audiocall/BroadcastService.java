package com.yesprojectsteam.audiocall;

import android.app.Notification;
import android.app.PendingIntent;
import android.app.Service;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.IBinder;
import android.telecom.PhoneAccountHandle;
import android.telecom.TelecomManager;
import android.util.Log;

public class BroadcastService extends Service {

    private final static String TAG = "BroadcastService";
    public PhoneAccountHandle phoneAccountHandle;
    CountDownTimer cdt = null;
    int timer = 10;

    @Override
    public void onCreate() {
        super.onCreate();

        Intent notificationIntent = new Intent(this, MainActivity.class);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0,
                notificationIntent, 0);
        Notification notification = new Notification.Builder(this)
                .setSmallIcon(android.R.drawable.ic_dialog_info)
                .setContentTitle("Ring A Ruse")
                .setContentText("Automode Count Down...")
                .setContentIntent(pendingIntent).build();

        startForeground(1317, notification);

        phoneAccountHandle = new PhoneAccountHandle(
                new ComponentName(getApplicationContext(),
                        VoiceCall.class), "Security App");
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.i(TAG, "onStartCommand");
        try {
            timer = intent.getIntExtra("timer", 0);
        }
        catch (Exception e) {
            Log.i(TAG, e.getMessage());
        }

        Log.i(TAG, "Starting timer...");
        cdt = new CountDownTimer(timer * 1000, 1000) {
            @Override
            public void onTick(long millisUntilFinished) {
                long timeremaining = millisUntilFinished / 1000;
                Log.i(TAG, "Countdown seconds remaining: " + timeremaining);
            }

            @Override
            public void onFinish() {
                Log.i(TAG, "Timer finished");
                Bundle callInfo = new Bundle();
                callInfo.putString("from","Security App");
                TelecomManager tm = (TelecomManager) getSystemService(Context.TELECOM_SERVICE);
                tm.addNewIncomingCall(phoneAccountHandle, callInfo);
            }
        };
        cdt.start();

        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public IBinder onBind(Intent arg0) {
        return null;
    }

    @Override
    public void onDestroy() {
        cdt.cancel();
        super.onDestroy();
    }
}
