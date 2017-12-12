getCandyByPriceMoreEqualThan(50, candy(Name, Price)).
getCandyByPriceLessThan(50, candy(Name, Price)).
getCookieByPriceMoreEqualThan(50, cookie(Name, Price)).
getCookieByPriceLessThan(50, cookie(Name, Price)).
getCakeByPriceMoreEqualThan(50, cake(Name, Ingredients, Price)).
getCakeByPriceLessThan(50, cake(Name, Ingredients, Price)).
getCakeByIngredientsPresent(["ingredient1", "ingredient2", ...], cake(Name, Ingredients, Price)).
getCakeByIngredientsAbsent(["ingredient1", "ingredient2", ...], cake(Name, Ingredients, Price)).