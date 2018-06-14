# SinglestoneCodeTest

This uses LiteDb so no configuration should be required. Simply open in Visual Studio as an administrator and build to your local browser. All API calls return json payloads intended for consumption by whatever would be calling this API.

API Calls (use rest client such as Postman for POST call):

GET:
/orders/get - returns all orders sorted by customer id then by order id (this is also the default routing)
/orders/get/{id} - returns the single order with id of {id}

POST:
/orders/create - create a new order; schema matches the assigned structure
