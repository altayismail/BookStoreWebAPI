# This is a .NET Core Web API Project which is called by me BookStoreWebAPI.
- There are Book, Author and Genre entities.
- There are GET, POST, PUT and DELETE operations for these entities.
- In Memory Database used.
- For data management, there is a DBOperations folder that includes DBContext, DataGenerator for In Memory Database and Interface for unit tests data.
- All unit tests are written for every bad and happy cases.
- API tests are done by Postman.
- For Authentication, JWT Bearer Token used.
- Application includes, Custom exception handler.
- As a middleware, Http request-respond code and situations asyncly written in console.
- Also error messages asyncly written in console.