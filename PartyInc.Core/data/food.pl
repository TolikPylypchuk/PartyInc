%facts(cakes)

%cake(cake name, ingredients, price for 1 cake)

cake("Funfetti cake",
     ["vanilla", "sugar"],
     174.5).

cake("Dark Prince",
     ["chocolate", "sugar"],
     150.0).

cake("Fruitty",
     ["banana", "strawberry", "pineapple", "apple", "orange"],
     128.0).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%facts(cookies)

%cookie(cookie name, price for 1 kl)

cookie("Americano", 44.5).

cookie("Strawberry Kifli", 56.0).

cookie("Chacolate Kifli", 64.2).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%facts(candies)

%candy(candy name, price for 1 kl)

candy("Winny Cherries", 150.8).

candy("Golden Nuts", 89.0).

candy("Snickers", 95.8).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%rules(cake, price)

getCakeByPriceLessThan(
		MaxPrice,
		cake(Name, Ingredients, Price)) :-
	cake(Name, Ingredients, Price),
	Price < MaxPrice.

getCakeByPriceMoreEqualThan(
		MinPrice,
		cake(Name, Ingredients, Price)) :-
	cake(Name, Ingredients, Price),
	MinPrice =< Price.

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%rules(cake, ingredients)

getCakeByIngredientsInclude(
		IngredientsInclude,
		cake(Name, Ingredients, Price)) :-
	cake(Name, Ingredients, Price),
	member(IngredientInclude, IngredientsInclude),
	member(IngredientInclude, Ingredients).
  
getCakeByIngredientsExclude(
		IngredientsExclude,
		cake(Name, Ingredients, Price)) :-
	cake(Name, Ingredients, Price),
	member(IngredientExclude, IngredientsExclude),
	not(member(IngredientExclude, Ingredients)).
 
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%rules(cookie, price)

getCookieByPriceLessThan(
		MaxPrice,
		cookie(Name, Price)) :-
	cookie(Name, Price),
	Price < MaxPrice.

getCookieByPriceMoreEqualThan(
		MinPrice,
		cookie(Name, Price)) :-
	cookie(Name, Price),
	MinPrice =< Price.

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%rules(candy, price)

getCandyByPriceLessThan(
		MaxPrice,
		candy(Name, Price)) :-
	candy(Name, Price),
	Price < MaxPrice.

getCandyByPriceMoreEqualThan(
		MinPrice,
		candy(Name, Price)) :-
	candy(Name, Price),
	MinPrice =< Price.
