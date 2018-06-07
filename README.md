# EComm Functions _(ongoing)_

This repository is developed using Azure serverless functions for ECommerce websites backend functioanlity.

Azure CosmosDB Table API has been setup in Azure cloud. Instructions can be found _[here](https://docs.microsoft.com/en-us/azure/cosmos-db/serverless-computing-database)_.

Current _API_ calls

**GET** http://localhost:7071/api/product/{category}/{rowkey}

Example GET Response
```json
{  
   "Name":"Victor Pasa T-Shirt",
   "Description":"T-shirt with blue strips with short seleves",
   "Category":"4364dfde-228d-3dgt-9eff-fdgr43223dd",
   "PartitionKey":"MensShirts",
   "RowKey":"1",
   "Timestamp":"2018-06-07T01:11:24.3735197+00:00",
   "ETag":"W/\"datetime'2018-06-07T01%3A11%3A24.3735197Z'\""
}
```


**POST** http://localhost:7071/api/product

Minimal information to send
```json
{
   "Name" :"Another T-sshirt",
   "Description" : "blue shit with one pocket",
   "Category" : "MensPants"
}
```

Example POST response

```json
{
   "Name": "Round neck T-shirt",
   "Description": "black one",
   "Category": "MensShirts",
   "PartitionKey": "MensShirts",
   "RowKey": "3611029c-228d-40d4-9eff-15ebecbe9681",
   "Timestamp": "0001-01-01T00:00:00+00:00",
   "ETag": "W/\"datetime'2018-06-07T02%3A34%3A17.0047724Z'\""
}
```