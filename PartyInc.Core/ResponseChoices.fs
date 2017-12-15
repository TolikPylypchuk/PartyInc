﻿[<RequireQualifiedAccess>]
module PartyInc.Core.ResponseChoices

let sweetsOrderConsultant =
    Map.empty
        .Add("order.all.price", 
            [
                "Okay. Then we can suggest you the following to order: {candies|cookies|cakes}."
                "All right. Then the following cakes will suit you: {candies|cookies|cakes}."
                "Good that. Taking into account what you have said, there are some suitable" + 
                    " variants. Here is the list: {candies|cookies|cakes}."
            ])
        .Add("order.cake", 
            [
                "Of course. Do you have any ingredients preferences?" +
                    " What about price?"
                "Okay. Are there ingredients you can't stand? Or, on the contrary," +
                    " like the most? Do you want also to specify price?"
                "Sure. Any ingredients preferences? Or maybe a price range?"
            ])
        .Add("order.cake.preferences-no", 
            [
                "Good that. Then any cakes will suit you. We have these: {cakes}"
            ])
        .Add("order.cake.preferences-yes", 
            [
                "Then tell me, please, what ingredients do you like the most?" +
                    " Or which ones you hate? Maybe you are allergic to anything?"
                "What ingredients would you prefer to be and not to be in a receipe?" +
                    " Are you allergic to anything?"
                "Okay then. What ingredients are your favourite? What you can't stand?" +
                    " Do you have an allergy?"
            ])
        .Add("order.cake.preferences-yes-dislikes", 
            [
                "Good. Is that all preferences you wanted to mention? If yes, what about the" +
                    " price?"
                "Okay, I got you. Are there any other specific ingredient's you want to tell me?" +
                    " Or do you want also to specify the price?"
                "Sure. That's it? In this case, what's the price range?"
            ])
        .Add("order.cake.preferences-yes-likes", 
            [
                "Okay. Nice choice. Is it all preferences you want to mention? If so, what about" +
                    " the price?"
                "We can do that. Great ingredient choice. Maybe, there are some other" +
                    " ingredient preferences you want to mention? If not, do you want also to" +
                    " specify the price?"
                "Nice. I like that myself, you know. That's it? Then what's the price range?"
            ])
        .Add("order.cake.preferences-yes-misunderstanding", 
            [
                "Sorry, what exactly do you mean? These ingridients: {indredients}, do you" +
                " like or dislike them? Or are you allergic to them?"
            ])
        .Add("order.cake.specify", 
            [
                "Good that. Do you want to order anything else?"
            ])
        .Add("order.stop", 
            [
                "Thank you for your order. Have a nice day!"
            ])
        .Add("order.sweets", 
            [
                "Of course. What about price per kilogram?"
                "Okay. Do you want also to specify price per kilogram?"
                "Sure. What's the price per kilogram range?"
            ])
        .Add("order.sweets.specify", 
            [
                "Good that. How much do you want?"
            ])
        .Add("order.sweets.weight", 
            [
                "Good that. Do you want to order anything else?"
            ])
        .Add("welcome",
            [
                "Good day! You are looking for some tasties, don't do?"
                "Hello! How can I help you?"
                "Greetings! What do you want to order?"
            ])
