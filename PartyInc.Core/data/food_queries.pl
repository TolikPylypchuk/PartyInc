%preferences
getCakeByIngredientsInclude(["ingredient1", "ingredient2", ...], cake(Name, Ingredients, Price)).
getCakeByIngredientsExclude(["ingredient1", "ingredient2", ...], cake(Name, Ingredients, Price)).

%price
getCakeByPriceLessThan(50, cake(Name, Ingredients, Price)).
getCakeByPriceMoreEqualThan(50, cake(Name, Ingredients, Price)).
getCandyByPriceLessThan(50, candy(Name, Price)).
getCandyByPriceMoreEqualThan(50, candy(Name, Price)).
getCookieByPriceLessThan(50, cookie(Name, Price)).
getCookieByPriceMoreEqualThan(50, cookie(Name, Price)).

%name
getCakeByName(Name, cake(Name, Ingredients, Price)).
getCandyByName(Name, candy(Name, Price)).
getCookieByName(Name, cookie(Name, Price)).

%getAll
cake(Name, Ingredients, Price).
candy(Name, Price).
cookie(Name, Price).