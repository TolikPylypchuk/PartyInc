getDrinkByName("tea", drink(Name, Ingredients, Price)).
getDrinkByName("coffee", drink(Name, Ingredients, Price)).
getDrinkByName("cocktail", drink(Name, Ingredients, Price)).
getDrinkByMaxPrice(40.0, drink(Name, Ingredients, Price)).
getDrinkByRangePrice(10.0, 40.0, drink(Name, Ingredients, Price)).
getDrinkByIngredients(["milk"], drink(Name, Ingredients, Price)).
getDrinkBySomeIngredients(["mint", "lemon"], drink(Name, AllIngredients, Price)).
getDrinkByNameAndSomeIngredients("cocktail", ["ice", "mint"], drink(Name, AllIngredients, Price)).
