# AutomaticTwitchMessagesSender
Simple console application that connects to your account and uses it to react to specifc messages

## Prerequisites
- Twitch account with at least 2FA
- an app registered inside the [Twitch developer portal](https://dev.twitch.tv/console) and its details
- access- and refresh-token following the [Twitch api documentation](https://dev.twitch.tv/docs/authentication/)
- creating launchSettings.json and add environment variables

## What is does?
After starting the console app it will connect to your account, join the designated chat and listen for messages, if a message contains what the code listens for, it will send a message into the chat. Designed for <ins>smaller</ins> chatrooms, has not been tested in bigger ones.
