%drinks
%drink(drinkName, ingredients, price for 1 drink)
drink("tea",
      [],
      5.0).

drink("tea",
      ["mint"],
      7.0).

drink("tea",
      ["mint", "lemon", "honey", "ginger"],
      15.0).

drink("coffee",
      [],
      18.0).

drink("coffee",
      ["milk"],
      20.0).

drink("cocktail",
      ["lemon", "ice", "mint"],
      25.0).

drink("cocktail",
      ["pineapple", "pitch", "ice", "lime", "cherry", "orange"],
      30.0).

%rules
getDrinkByName(DrinkName, drink(DrinkName, Ingredients, Price)):-
    drink(DrinkName, Ingredients, Price).

getDrinkByMaxPrice(MaxPrice, drink(Name, Ingredients, Price)):-
    drink(Name, Ingredients, Price),
    Price =< MaxPrice.

getDrinkByRangePrice(MinPrice, MaxPrice, drink(Name, Ingredients, Price)):-
    drink(Name, Ingredients, Price),
    Price >= MinPrice,
    Price =< MaxPrice.

getDrinkByIngredients(Ingredients, drink(Name, Ingredients, Price)):-
    drink(Name, Ingredients, Price).

getDrinkBySomeIngredients(SomeIngredients, drink(Name, AllIngredients, Price)):-
    drink(Name, AllIngredients, Price),
    member(Ingredient, SomeIngredients),
    member(Ingredient, AllIngredients).

getDrinkByNameAndSomeIngredients(Name, SomeIngredients, drink(Name, AllIngredients, Price)):-
    drink(Name, AllIngredients, Price),
    member(Ingredient, SomeIngredients),
    member(Ingredient, AllIngredients).