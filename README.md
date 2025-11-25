# AutomaticTwitchMessagesSender
Simple console application that connects to your account and uses it to react to specifc messages

## Prerequisites
- Twitch account with at least 2FA
- an app registered inside the Twitch developer portal and it's details
- access- and refresh-token following the [twitch api documentation](https://dev.twitch.tv/docs/authentication/)
- creating launchSettings.json and add environment variables

## What is does?
After starting the console app it will connect to your account and join the designated chat and listen for messages, if a message contains what the code listens form it will fire a request to the helix Twitch api which will try to send a message. Might run into StatusCode 429 Too Many Requests because of missing request throttling. Designed for <ins>smaller</ins> chatrooms.
