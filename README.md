# OrderSystemAPI
An improved version of Pet_Store_Order_API. Accepts a Post Request with order entry data and makes HTTP call to an Inventory API to acquire product data. Takes data from both sources to create an order summary.

The OrderSystemAPI project recieves an order entry request made up of a customer id and an array of items consiting of product ids and quantity. It will take this data and make an HTTP GET request to InventoryAPI to retrieve the product's name and price. With this information, OrderSystemAPI will make a full order summary including a total cost. 

The ClientApp includes an HTML webpage to see this order summary on any browser. 

Below is an example of the JSON order entry request:

{
  "customerId": "12345",
  "items": [
    {
      "productId": "8ed0e6f7",
      "quantity": 1
    },
    {
      "productId": "c0258525",
      "quantity": 3
    },
    {
      "productId": "0a207870",
      "quantity": 2
    }
  ]
}

This Project is using the SQL Server on Visual Studio 2017 for its database.

# STEPS TO SET UP DATABASES
- Open the OrderSystemAPI project
- Open the Package Manager Console 
- Type "Drop-Databse" in the PM console
- Then type "Update-Database". This will upload the databases under the OrderAPI database folder.
- Open the InventoryAPI project 
- repeat Steps 2-4 to drop and update the database
- Open the SQL Server Object Extension window in VS to view the DB tables for the OrderAPI and ProductAPI DBs, respectively, under the MSSQLLocalDB folder.

# ENTER AN ORDER ENTRY 
  - Open Postman (or a software similar for making HTTP requests)
  - Enter a POST Request with the following URL: https://localhost:44310/api/order
  - Copy-and-Paste the JSON Entry example above into the Body section. Click the 'raw' button and select JSON as the text type. 
  - Click the blue SEND button and it should return a 201 Created status code along with a full order summary as seen below: 
 
  {
    "orderID": "9717bddb-e61e-4ebb-baeb-3c95737261a1",
    "orderEntry": {
        "customerID": "12345",
        "items": [
            {
                "productID": "8ed0e6f7",
                "quantity": 1
            },
            {
                "productID": "c0258525",
                "quantity": 3
            },
            {
                "productID": "0a207870",
                "quantity": 2
            }
        ]
    },
    "products": [
        {
            "productID": "8ed0e6f7",
            "productName": "Dog Food",
            "productPrice": 12.99
        },
        {
            "productID": "c0258525",
            "productName": "Cat Food",
            "productPrice": 10.49
        },
        {
            "productID": "0a207870",
            "productName": "Dog Leash",
            "productPrice": 5.99
        }
    ],
    "total": 56.44
}

# VIEW THE ORDER DETAILS 
  -Go to Client API project.
  -Open the 'wwwroot' folder. 
  -Right-click on the index.html file and click on 'View in Browser'. 
  
  The HTML page will display a list of order IDs currently in the DB. Below it is a text box to enter an Order ID to display
  that order's summary (list of products, quantity, and Total Cost). 
  
  # ENDPOINTS 
  
  - (POST) https://localhost:44310/api/order
    - creates an order, if included with JSON body
  - (GET) https://localhost:44310/api/order
    - retrieves all orders in the DB
  - (GET) https://localhost:44310/api/order/{id}
    - retrieves specific order from DB with id being the Order ID or Customer ID
  - (PUT) https://localhost:44310/api/order/{id}
    - updates a full order in the DB, if correct order ID is provided in URL as well as correct JSON string in the body section
  - (DELETE) https://localhost:44310/api/order/{id}
    - deletes a full order and its corresponding data from the DB, if correct order ID is provided in the link
