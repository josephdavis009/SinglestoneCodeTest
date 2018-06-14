# SinglestoneCodeTest

This uses LiteDb so no configuration should be required. Simply open in Visual Studio and run in local browser. All API calls return json payloads intended for consumption by whatever is calling this API.

API Calls (use rest client such as Postman for POST call):

GET:
/orders/get - returns all orders sorted by customer id then by order id (this is also the default routing)
/orders/get/{id} - returns the single order with id of {id}
POST:
/orders/create - create a new order; schema matches the assigned structure
