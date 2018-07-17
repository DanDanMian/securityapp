package com.yesprojectsteam.audiocall;

import android.content.Intent;
import android.net.Uri;
import android.telecom.Connection;
import android.telecom.ConnectionRequest;
import android.telecom.ConnectionService;
import android.telecom.DisconnectCause;
import android.telecom.PhoneAccountHandle;
import android.telecom.TelecomManager;
import android.util.Log;

import static android.content.ContentValues.TAG;

public class VoiceCall extends ConnectionService {

    @Override
    public Connection onCreateIncomingConnection(final PhoneAccountHandle connectionManagerPhoneAccount, final ConnectionRequest request) {
        Log.d(TAG,"onCreateIncomingConnection");
        final Connection connection = new Connection() {
            @Override
            public void onAnswer() {
                //this.setActive();
                DisconnectCause cause = new DisconnectCause(DisconnectCause.OTHER);
                this.setDisconnected(cause);
                this.destroy();
                Intent launchIntent = getPackageManager().getLaunchIntentForPackage("com.yesproject.Security");
                if (launchIntent != null) {
                    startActivity(launchIntent);
                }
                else {
                    Log.i(TAG, "Launch Intent For Package Not Found");
                }
            }

            @Override
            public void onReject() {
                DisconnectCause cause = new DisconnectCause(DisconnectCause.REJECTED);
                this.setDisconnected(cause);
                this.destroy();
            }

            @Override
            public void onDisconnect() {
                DisconnectCause cause = new DisconnectCause(DisconnectCause.LOCAL);
                this.setDisconnected(cause);
                this.destroy();
            }

            @Override
            public void onAbort() {
                super.onAbort();
            }
        };
        connection.setAddress(Uri.parse(request.getExtras().getString("from")), TelecomManager.PRESENTATION_ALLOWED);
        connection.setAudioModeIsVoip(true);
        connection.setConnectionCapabilities(Connection.CAPABILITY_MUTE);
        connection.setInitializing();
        return connection;
    }
}