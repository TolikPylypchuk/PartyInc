[<RequireQualifiedAccess>]
module PartyInc.Core.ResponseChoices

let sweetsOrderConsultant =
    Map.empty
        .Add("None",
            [
                "Sorry. Can you repeat, please?"
            ])
        .Add("order.cakes.preferences-no", 
            [
                "Good that. Then any %sweets will suit you."
            ])
        .Add("order.cakes.preferences-yes", 
            [
                "Then tell me, please, what ingredients do you like the most?" +
                    " Or which ones you hate? Maybe you are allergic to anything?";
                "What ingredients would you prefer to be and not to be in a receipe?" +
                    " Are you allergic to anything?";
                "Okay then. What ingredients are your favourite? What you can't stand?" +
                    " Do you have an allergy?"
            ])
        .Add("order.cakes.preferences-yes-dislikes", 
            [
                "Good it is. Maybe, there are other ingredients you definitely like and want" +
                    " to be in a receipe? Maybe some other you don't like? Or it is all" +
                    " preferences you want to mention? Then what about weight and price?";
                "Okay, I got you. What about any other ingredients you prefer to be or not" +
                    " to be in a receipe? Or there are no other specific ingredient's you" +
                    " want to tell me? Then what about weight and price?";
                "Sure. Maybe, you have thought about any other ingredients you definitely" +
                    " don't want to be in a receipe? Or any that you want? Or that's it?" +
                    " Then what about weight and price?"
            ])
        .Add("order.cakes.preferences-yes-likes", 
            [
                "Okay. Nice choice. What about any other ingredients you prefer to be" +
                    " or not to be in a receipe? Or it is all preferences you want to" +
                    " mention? Then what about weight and price?";
                "We can do that. Great ingredient choice. Maybe, there are some other" +
                    " ingredients you like? Or dislike? Or there are no other specific" +
                    " ingredients you want to tell me? Then what about weight and price?";
                "Sure. I like that myself, you know. Maybe, there are any ingredients you" +
                    " are allergic to? Or you just don't like? Or, on contrary, like? Or" +
                    " that's it? Then what about weight and price?"
            ])
        .Add("order.cakes.preferences-yes-misunderstanding", 
            [
                "Sorry, what exactly do you mean? These ingridients: %indredients, do you" +
                " like or dislike them? Or are you allergic to them?"
            ])
        .Add("order.cakes.preferences-yes-stop", 
            [
                "Okay. Then we can suggest you the following %sweets to order.";
                "All right. Then the following %sweets will suit you.";
                "Good that. Taking into account what you have said, there are some suitable" + 
                " %sweets."
            ])
        .Add("order.sweets", 
            [
                "Of course. Do you have any ingredients preferences?" +
                    " What about price and weight?";
                "Okay. Are there ingredients you can't stand? Or, on the contrary," +
                    " like the most? Do you want also to specify weight? Or price?";
                "Sure. Any ingredients preferences? Or maybe price? Or weight?"
            ])
        .Add("order.sweets.price", 
            [
                "Okay. Anything else?"
            ])
        .Add("order.sweets.weight", 
            [
                "Good that. Anything else?"
            ])
        .Add("welcome", 
            [
                "Good day! You are looking for some tasties, don't do?";
                "Hello! How can I help you?";
                "Greetings! What do you want to order?"
            ])
