import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        //appbar theme --------------------------------------------------------
        appBarTheme: AppBarTheme(
          backgroundColor: Color.fromARGB(255, 83, 49, 28),
          titleTextStyle: TextStyle(
            fontSize: 40,
            color: Color.fromARGB(255, 197, 127, 81),
          ),
        ),
        //text themes ---------------------------------------------------------
        textTheme: TextTheme(
          //popup food name title and about title
          titleLarge: TextStyle(fontSize: 40, color: Colors.black),
          //popup ingredients and recipe title
          titleMedium: TextStyle(fontSize: 24, color: Colors.black),
          //display food item name
          titleSmall: TextStyle(
            fontSize: 16,
            color: Color.fromARGB(255, 197, 127, 81),
          ),
          //controls headline and search button
          bodyLarge: TextStyle(fontSize: 16, color: Colors.black),
          //popup ingredient and recipe info and close button and about content
          bodyMedium: TextStyle(fontSize: 14, color: Colors.black),
          //controls description and control fields
          bodySmall: TextStyle(fontSize: 12, color: Colors.black),
          //validation
          labelLarge: TextStyle(fontSize: 12, color: Colors.red),
        ),
        fontFamily: "Inter",
        //colors -------------------------------------------------------------
        //appbar background and food item background
        primaryColor: Color.fromARGB(255, 83, 49, 28),
        //display background
        primaryColorDark: Color.fromARGB(255, 30, 27, 23),
        //appbar text and food item name text
        primaryColorLight: Color.fromARGB(255, 197, 127, 81),
        //controls and popup background
        canvasColor: Color.fromARGB(255, 170, 170, 170),
        //controls background and close button
        splashColor: Color.fromARGB(255, 217, 217, 217),

        scaffoldBackgroundColor: Color.fromARGB(255, 217, 217, 217),
      ),
      home: MyGiphy(),
    );
  }
}

class MyGiphy extends StatefulWidget {
  const MyGiphy({super.key});

  @override
  State<MyGiphy> createState() => _MyGiphyState();
}

class _MyGiphyState extends State<MyGiphy> {
  //saved data stuff
  late SharedPreferences myPrefs;

  //text field stuff
  String letter = "";
  final txtLetterController = TextEditingController();
  final letterFormId = GlobalKey<FormState>();

  //amount dropdown stuff
  int amount = 0;
  List<DropdownMenuItem> amountList = [
    DropdownMenuItem(value: 0, child: Text("Show all")),
    DropdownMenuItem(value: 1, child: Text("1")),
    DropdownMenuItem(value: 5, child: Text("5")),
    DropdownMenuItem(value: 10, child: Text("10")),
    DropdownMenuItem(value: 15, child: Text("15")),
    DropdownMenuItem(value: 20, child: Text("20")),
  ];

  //categories dropdown stuff
  String category = "Show all";
  List<DropdownMenuItem> categoryList = [
    DropdownMenuItem(value: "Show all", child: Text("Show all")),
    DropdownMenuItem(value: "Beef", child: Text("Beef")),
    DropdownMenuItem(value: "Chicken", child: Text("Chicken")),
    DropdownMenuItem(value: "Dessert", child: Text("Dessert")),
    DropdownMenuItem(value: "Lamb", child: Text("Lamb")),
    DropdownMenuItem(value: "Miscellaneous", child: Text("Miscellaneous")),
    DropdownMenuItem(value: "Pasta", child: Text("Pasta")),
    DropdownMenuItem(value: "Pork", child: Text("Pork")),
    DropdownMenuItem(value: "Seafood", child: Text("Seafood")),
    DropdownMenuItem(value: "Side", child: Text("Side")),
    DropdownMenuItem(value: "Starter", child: Text("Starter")),
    DropdownMenuItem(value: "Vegan", child: Text("Vegan")),
    DropdownMenuItem(value: "Vegetarian", child: Text("Vegetarian")),
    DropdownMenuItem(value: "Breakfast", child: Text("Breakfast")),
    DropdownMenuItem(value: "Goat", child: Text("Goat")),
  ];

  //area dropdown stuff
  String area = "Show all";
  List<DropdownMenuItem> areaList = [
    DropdownMenuItem(value: "Show all", child: Text("Show all")),
    DropdownMenuItem(value: "Algerian", child: Text("Algerian")),
    DropdownMenuItem(value: "American", child: Text("American")),
    DropdownMenuItem(value: "Argentinian", child: Text("Argentinian")),
    DropdownMenuItem(value: "Australian", child: Text("Australian")),
    DropdownMenuItem(value: "British", child: Text("British")),
    DropdownMenuItem(value: "Canadian", child: Text("Canadian")),
    DropdownMenuItem(value: "Chinese", child: Text("Chinese")),
    DropdownMenuItem(value: "Croatian", child: Text("Croatian")),
    DropdownMenuItem(value: "Dutch", child: Text("Dutch")),
    DropdownMenuItem(value: "Egyptian", child: Text("Egyptian")),
    DropdownMenuItem(value: "Filipino", child: Text("Filipino")),
    DropdownMenuItem(value: "French", child: Text("French")),
    DropdownMenuItem(value: "Greek", child: Text("Greek")),
    DropdownMenuItem(value: "Indian", child: Text("Indian")),
    DropdownMenuItem(value: "Irish", child: Text("Irish")),
    DropdownMenuItem(value: "Italian", child: Text("Italian")),
    DropdownMenuItem(value: "Jamaican", child: Text("Jamaican")),
    DropdownMenuItem(value: "Japanese", child: Text("Japanese")),
    DropdownMenuItem(value: "Kenyan", child: Text("Kenyan")),
    DropdownMenuItem(value: "Malaysian", child: Text("Malaysian")),
    DropdownMenuItem(value: "Mexican", child: Text("Mexican")),
    DropdownMenuItem(value: "Moroccan", child: Text("Moroccan")),
    DropdownMenuItem(value: "Norwegian", child: Text("Norwegian")),
    DropdownMenuItem(value: "Polish", child: Text("Polish")),
    DropdownMenuItem(value: "Portuguese", child: Text("Portuguese")),
    DropdownMenuItem(value: "Russian", child: Text("Russian")),
    DropdownMenuItem(value: "Saudi Arabian", child: Text("Saudi Arabian")),
    DropdownMenuItem(value: "Slovakian", child: Text("Slovakian")),
    DropdownMenuItem(value: "Spanish", child: Text("Spanish")),
    DropdownMenuItem(value: "Syrian", child: Text("Syrian")),
    DropdownMenuItem(value: "Thai", child: Text("Thai")),
    DropdownMenuItem(value: "Tunisian", child: Text("Tunisian")),
    DropdownMenuItem(value: "Turkish", child: Text("Turkish")),
    DropdownMenuItem(value: "Ukrainian", child: Text("Ukrainian")),
    DropdownMenuItem(value: "Uruguayan", child: Text("Uruguayan")),
    DropdownMenuItem(value: "Venezulan", child: Text("Venezulan")),
    DropdownMenuItem(value: "Vietnamese", child: Text("Vietnamese")),
  ];

  //recipe stuff
  List<Map<String, dynamic>> recipes = [];
  String result = "Hit search to find results";

  //starts the program
  @override
  void initState() {
    // TODO: implement initState
    super.initState();

    //adds listener
    txtLetterController.addListener(() {
      setState(() {
        letter = txtLetterController.text;
      });
    });

    //gets preferences
    init();
  }

  //disposes variables so the stack doesn't overflow
  @override
  void dispose() {
    // TODO: implement dispose
    txtLetterController.dispose();

    super.dispose();
  }

  //gets preferences on load
  Future<void> init() async {
    myPrefs = await SharedPreferences.getInstance();
    await loadPrefs();
  }

  //saves preferences such as the first letter text field
  Future<void> savePrefs() async {
    // Save regular text fields
    await myPrefs.setString("letter", txtLetterController.text);
    // await myPrefs.setInt("amount", amount);
    // await myPrefs.setString("category", category);
    // await myPrefs.setString("area", area);
  }

  //loads preferences such as the first letter text field
  Future<void> loadPrefs() async {
    // Load regular text fields
    String loadedLetter = myPrefs.getString("letter") ?? "";
    // int loadedAmount = myPrefs.getInt("amount") ?? 0;
    // String loadedCategory = myPrefs.getString("category") ?? "Show all";
    // String loadedArea = myPrefs.getString("area") ?? "Show all";

    setState(() {
      txtLetterController.text = loadedLetter;
      // amount = loadedAmount;
      // category = loadedCategory;
      // area = loadedArea;
    });
  }

  //calls the api for all the recipes that start with a certain letter
  Future<void> getRecipes(String url) async {
    //tries to get data
    try {
      var response = await http.get(Uri.parse(url));
      if (response.statusCode == 200) {
        var jsonResponse = jsonDecode(response.body);
        //print(jsonResponse);

        List<Map<String, dynamic>> tempList = [];

        //loops through all the recipes starting with that letter
        for (final recipe in jsonResponse["meals"]) {
          //if the category or area dropdown is selected to show all then the filter isn't applied
          //if the selected category or area dropdown matches the recipe then it is added to the list
          if ((category == "Show all" || recipe["strCategory"] == category) &&
              (area == "Show all" || recipe["strArea"] == area)) {
            tempList.add(recipe);
          }
        }

        //if the amount equals 0 that means show all so all recipes with applied filters are shown
        //otherwise the list is reduced to either the max amount if its smaller than the selected amount or it is the selected amount
        if (amount != 0) {
          tempList = tempList.take(amount).toList();
        }

        //updates the list and result text
        setState(() {
          recipes = tempList;
          result = "${recipes.length} results found";
        });
      } else {
        //updates the list and result text
        setState(() {
          recipes = [];
          result = "0 results found";
        });
      }
    } catch (e) {
      print("Network Error: $e");
    }
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        FocusManager.instance.primaryFocus?.unfocus();
      },
      child: Scaffold(
        //appbar ----------------------------------------------------------------
        appBar: AppBar(
          title: Text(
            "Recipe Finder",
            style: Theme.of(context).appBarTheme.titleTextStyle,
          ),
          leading: Padding(padding: EdgeInsetsGeometry.all(10)),
          actions: [
            IconButton(
              onPressed: () {
                showDialog(
                  context: context,
                  builder: (context) {
                    //about alert dialog ----------------------------------------------
                    return AlertDialog(
                      title: Text('About'),
                      backgroundColor: Theme.of(context).canvasColor,
                      contentTextStyle: Theme.of(context).textTheme.bodyMedium,
                      content: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Text("App Name: Recipe Finder"),
                          SizedBox(height: 10),
                          Text(
                            "Description: The purpose of this app is to offer people who cook a way to search up recipes",
                          ),
                          SizedBox(height: 10),
                          Text("Developer: Shaelin Brown"),
                          SizedBox(height: 10),
                          Text("API Name: TheMealDB"),
                          SizedBox(height: 10),
                          Text("API Link: https://www.themealdb.com/api.php"),
                          SizedBox(height: 10),
                          Text("Credits:"),
                          SizedBox(height: 5),
                          Text(
                            "- Inter Font - https://fonts.google.com/specimen/Inter",
                          ),
                          SizedBox(height: 5),
                          Text(
                            "- Documentation - https://github.com/jptweb/IGME-340-Shared/blob/main/reference/README.md",
                          ),
                          SizedBox(height: 5),
                          Text("- Documentation - https://dart.dev/docs"),
                          SizedBox(height: 10),

                          //close button ---------------------------------------------
                          Center(
                            child: ElevatedButton(
                              style: ElevatedButton.styleFrom(
                                backgroundColor: Theme.of(context).splashColor,
                                foregroundColor: Colors.black,
                                textStyle: Theme.of(
                                  context,
                                ).textTheme.bodyMedium,
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadiusGeometry.circular(
                                    5,
                                  ),
                                ),
                              ),
                              onPressed: () => Navigator.pop(context),
                              child: Text("Close"),
                            ),
                          ),
                        ],
                      ),
                    );
                  },
                );
              },
              icon: Icon(Icons.info),
              color: Color.fromARGB(255, 197, 127, 81),
            ),
          ],
        ),
        body: Column(
          children: [
            //Controls -----------------------------------------------------------------------
            Container(
              padding: EdgeInsets.all(10),
              child: Column(
                children: [
                  //description -----------------------------------------------
                  Text(
                    "Filter through recipes using the controls below.",
                    style: Theme.of(context).textTheme.bodyLarge,
                  ),

                  SizedBox(height: 10),

                  //Letter textfield ------------------------------------------
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "Enter the first letter of the food",
                        style: Theme.of(context).textTheme.bodySmall,
                      ),

                      Container(
                        width: 150,
                        child: Form(
                          key: letterFormId,
                          autovalidateMode: AutovalidateMode.onUserInteraction,
                          child: TextFormField(
                            controller: txtLetterController,
                            //makes sure the value is 1 letter
                            validator: (value) {
                              //makes sure something is in the field
                              if (value == null || value.isEmpty) {
                                return "Field is required";
                              }
                              //makes sure it's 1 letter
                              if (!RegExp(r'^[a-zA-Z]$').hasMatch(value)) {
                                return 'Enter 1 letter';
                              }
                              return null;
                            },
                            decoration: InputDecoration(
                              labelText: "First Letter",
                              labelStyle: Theme.of(context).textTheme.bodySmall,
                              errorStyle: Theme.of(
                                context,
                              ).textTheme.labelLarge,
                              fillColor: Theme.of(context).canvasColor,
                              filled: true,
                              border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(5),
                                borderSide: BorderSide.none,
                              ),
                              //clear button
                              suffixIcon: IconButton(
                                icon: Icon(Icons.clear),
                                onPressed: () {
                                  txtLetterController.clear();
                                },
                              ),
                            ),
                            style: Theme.of(context).textTheme.bodySmall,
                          ),
                        ),
                      ),
                    ],
                  ),

                  SizedBox(height: 10),

                  //Amount dropdown -------------------------------------------
                  makeDropdown(
                    context,
                    "Select the amount of recipes to view",
                    amount,
                    amountList,
                    (selected) {
                      amount = selected;
                    },
                  ),

                  SizedBox(height: 10),

                  //Categories dropdown ---------------------------------------
                  makeDropdown(
                    context,
                    "Select the type of recipe to view",
                    category,
                    categoryList,
                    (selected) {
                      category = selected;
                    },
                  ),

                  SizedBox(height: 10),

                  //Area dropdown ---------------------------------------------
                  makeDropdown(
                    context,
                    "Select the area the recipe is from",
                    area,
                    areaList,
                    (selected) {
                      area = selected;
                    },
                  ),

                  SizedBox(height: 10),

                  //Search Button ---------------------------------------------
                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Theme.of(context).canvasColor,
                      foregroundColor: Colors.black,
                      textStyle: Theme.of(context).textTheme.bodyLarge,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadiusGeometry.circular(5),
                      ),
                    ),
                    onPressed: () async {
                      if (letterFormId.currentState!.validate()) {
                        FocusManager.instance.primaryFocus?.unfocus();
                        await savePrefs();
                        // print("Saved First Letter: $letter");
                        // print("Amount: $amount");
                        // print("Category: $category");
                        // print("Area: $area");

                        String url =
                            "https://www.themealdb.com/api/json/v1/1/search.php?f=$letter";
                        await getRecipes(url);
                      }
                    },
                    child: const Text("Search"),
                  ),

                  SizedBox(height: 10),

                  //Results text ----------------------------------------------
                  Text(result, style: Theme.of(context).textTheme.bodyLarge),
                ],
              ),
            ),

            //Recipies Display -----------------------------------------------------------------------
            Container(
              child: Expanded(
                child: Container(
                  padding: EdgeInsets.all(35),
                  color: Theme.of(context).primaryColorDark,
                  //builds a gridview for each recipe and displays the name and image
                  child: GridView.builder(
                    gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                      crossAxisCount: 2,
                      crossAxisSpacing: 35.0,
                      mainAxisSpacing: 35.0,
                    ),
                    itemCount: recipes.length,
                    itemBuilder: (context, index) {
                      //makes a clickable widget that can trigger an about dialog to show futher details about the recipe
                      return InkWell(
                        onTap: () {
                          showDialog(
                            context: context,
                            builder: (context) {
                              return makeRecipeAlertDialog(
                                context,
                                recipes[index]["strMeal"],
                                recipes[index]["strMealThumb"],
                                getIngredients(recipes[index]),
                                recipes[index]["strInstructions"],
                              );
                            },
                          );
                        },
                        child: Stack(
                          children: [
                            Container(
                              width: 150,
                              height: 150,
                              color: Theme.of(context).primaryColor,
                              child: Column(
                                children: [
                                  Container(
                                    height: 30,
                                    padding: EdgeInsets.all(5),
                                    //makes the text scrollable horizaontally
                                    child: SingleChildScrollView(
                                      scrollDirection: Axis.horizontal,
                                      child: Text(
                                        recipes[index]["strMeal"],
                                        style: Theme.of(
                                          context,
                                        ).textTheme.titleSmall,
                                      ),
                                    ),
                                  ),

                                  Image.network(
                                    recipes[index]["strMealThumb"],
                                    width: 115,
                                    height: 115,
                                  ),
                                ],
                              ),
                            ),
                          ],
                        ),
                      );
                    },
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  //gets the ingredients and correlated measurements for a specific recipe and returns a list that contains the pairs in maps
  List<Map<String, String>> getIngredients(Map<String, dynamic> recipe) {
    //creates empty list
    List<Map<String, String>> ingredients = [];

    //loops though all the ingredients
    for (int i = 1; i <= 20; i++) {
      String itemKey = "strIngredient$i";
      String measurementKey = "strMeasure$i";

      String ingredient = recipe[itemKey];
      String measurement = recipe[measurementKey];

      //breaks out of the loop if an ingredient is empty
      if (ingredient.trim().isEmpty) {
        break;
      }

      //if it's not empty it gets added to the list
      ingredients.add({
        "ingredient": ingredient.trim(),
        "measurement": measurement.trim(),
      });
    }

    //returns a list full of ingredients and measurements
    return ingredients;
  }

  //makes the about dialog for the specific recipe you click
  AlertDialog makeRecipeAlertDialog(
    BuildContext context,
    String title,
    String imgUrl,
    List<Map<String, String>> ingredients,
    String recipe,
  ) {
    return AlertDialog(
      title: Text(
        title,
        style: Theme.of(context).textTheme.titleLarge,
        textAlign: TextAlign.center,
      ),
      backgroundColor: Theme.of(context).canvasColor,
      //makes it scrollable
      content: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisSize: MainAxisSize.min,
          children: [
            //image -----------------------------------------------------
            Image.network(imgUrl, width: 250, height: 250),

            SizedBox(height: 10),

            //ingredients -----------------------------------------------
            Text("Ingredients", style: Theme.of(context).textTheme.titleMedium),

            SizedBox(height: 10),

            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              //takes the list and turns each item into a text widget
              children: ingredients.map((item) {
                return Text(
                  ("${item["measurement"]} ${item["ingredient"]}").trim(),
                  style: Theme.of(context).textTheme.bodyMedium,
                );
              }).toList(),
            ),
            SizedBox(height: 10),

            //recipe ----------------------------------------------------
            Text("Recipe", style: Theme.of(context).textTheme.titleMedium),

            SizedBox(height: 10),

            Text(
              recipe,
              style: Theme.of(context).textTheme.bodyMedium,
              textAlign: TextAlign.start,
            ),

            SizedBox(height: 10),

            //close button ---------------------------------------------
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                backgroundColor: Theme.of(context).splashColor,
                foregroundColor: Colors.black,
                textStyle: Theme.of(context).textTheme.bodyMedium,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadiusGeometry.circular(5),
                ),
              ),
              onPressed: () => Navigator.pop(context),
              child: Text("Close"),
            ),
          ],
        ),
      ),
    );
  }

  //makes the dropdown controls and returns a row
  Row makeDropdown(
    BuildContext context,
    String description,
    dynamic valueVar,
    List<DropdownMenuItem> itemsVar,
    //need to pass in function so the variables change by reference
    Function(dynamic) onChangedFunction,
  ) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        //description -----------------------------------------------
        Text(description, style: Theme.of(context).textTheme.bodySmall),

        //dropdown --------------------------------------------------
        Container(
          width: 150,
          padding: EdgeInsets.symmetric(horizontal: 5),
          decoration: BoxDecoration(
            color: Theme.of(context).canvasColor,
            borderRadius: BorderRadius.circular(5),
          ),
          child: DropdownButton(
            value: valueVar,
            items: itemsVar,
            isExpanded: true,
            style: Theme.of(context).textTheme.bodySmall,
            onChanged: (selected) {
              setState(() {
                onChangedFunction(selected);
              });
            },
          ),
        ),
      ],
    );
  }
}
