# Moonpig Engineering recruitment test

Welcome to the Moonpig engineering test. Here at Moonpig we really value
quality code, and this test has been designed to allow you to show us how 
you think quality code should be written. 

To allow you to focus on the design and implementation of the code we have 
added all the use cases we expect you to implement to the bottom of the 
instructions. In return we ask that you make sure your implementation 
follows all the best practices you are aware of, and that at the end of it, 
the code you submit, is code you are proud of. 

We have not set a time limit, we prefer that you spend some extra time to get
it right and write the highest quality code you can. Please feel free to make
any changes you want to the solution, add classes, remove projects etc.

We are interested in seeing how you approach the task so please commit more
regularly than you normally would so we can see each step and **include
the .git folder in your submission**.

When complete please upload your solution and answers in a .zip to the google
drive link provided to you by the recruiter.

## Programming Exercise - Moonpig Post Office

You have been tasked with creating a service that calculates the estimated 
despatch dates of customers' orders. 

An order consists of an order date and a collection of products that a 
customer has added to their shopping basket. 

Each of these products is supplied to Moonpig on demand through a number of 
3rd party suppliers.

As soon as an order is received by a supplier, the supplier will start 
processing the order. The supplier has an agreed lead time in which to 
process the order before delivering it to the Moonpig Post Office.

Once the Moonpig Post Office has received all products in an order it is 
despatched to the customer.

**Assumptions**:

1. Suppliers start processing an order on the same day that the order is 
	received. For example, a supplier with a lead time of one day, receiving
	an order today will send it to Moonpig tomorrow.

2. For the purposes of this exercise we are ignoring time i.e. if a 
	supplier has a lead time of 1 day then an order received any time on 
	Tuesday would arrive at Moonpig on the Wednesday.

3. Once all products for an order have arrived at Moonpig from the suppliers,
	they will be despatched to the customer on the same day.

### Part 1 

When the /api/DespatchDate endpoint is hit return the despatch date of that
order.

### Part 2

Moonpig Post Office staff are getting complaints from customers expecting
packages to be delivered on the weekend. You find out that the Moonpig post
office is shut over the weekend. Packages received from a supplier on a
weekend will be despatched the following Monday.

Modify the existing code to ensure that any orders received from a supplier
on the weekend are despatched on the following Monday.

### Part 3

The Moonpig post office is still getting complaints... It turns out suppliers
don't work during the weekend as well, i.e. if an order is received on the
Friday with a lead time of 2 days, Moonpig would receive and despatch on the
Tuesday.

Modify the existing code to ensure that any orders that would have been 
processed during the weekend resume processing on Monday.

---

Parts 1 & 2 have already been completed albeit lacking in quality. Please
review the code, document the problems you find (see question 1), and refactor
into what you would consider quality code.

Once you have completed the refactoring, extend your solution to capture the 
requirements listed in part 3.

Please note, the provided DbContext is a stubbed class which provides test 
data. Please feel free to use this in your implementation and tests but do 
keep in mind that it would be switched for something like an EntityFramework 
DBContext backed by a real database in production.

While completing the exercise please answer the questions listed below. 
We are not looking for essay length answers. You can add the answers in this 
document.

## Questions

Q1. What 'code smells' / anti-patterns did you find in the existing 
	implementation of part 1 & 2?
    Creating a new DbContext instance inside a loop can lead to performance issues and potential resource leaks. Creating and using a single DbContext instance throughout the method is better.
    There is no error handling for database queries. A product or supplier will throw an exception if it is not found. It's better to handle these scenarios gracefully.
    The values like 2 and 1 in AddDays are magic numbers. Using named constants or variables is better to make the code more readable and maintainable.
    The method does multiple things (retrieve data from the database, calculate lead time, and determine dispatch date). It's better to break it down into smaller, more focused methods.
    Using a global variable _mlt makes the method less readable and harder to understand. Consider passing it as a parameter or using it as a return value.
    Comparing DateTime objects using == might not be accurate due to potential differences in milliseconds. It's better to use methods like DateTime.Equals or compare ticks.

Q2. What best practices have you used while implementing your solution?
    Used dependency injection to inject the DbContext into the DispatchDateCalculator class, improving testability and flexibility.
    I separated the logic for calculating the dispatch date and adjusting for weekends into distinct methods within the DispatchDateCalculator class, adhering to the Single Responsibility Principle.
    Followed consistent naming conventions for variables and methods, enhancing code readability, communicating their purpose, and making the code more readable and self-explanatory.
    Implemented guard clauses to check for null values and handle exceptional cases early, improving code robustness.
    Followed consistent naming conventions for variables and methods, enhancing code readability.
    Added comments where necessary to explain complex logic or provide additional context.

Q3. What further steps would you take to improve the solution given more time?
    Develop comprehensive unit tests to validate the behaviour of the LeadTimeCalculator class under various scenarios.
    Introduce logging to capture relevant information, such as exceptions or significant events, for monitoring and debugging purposes.
    Enhance error handling by providing meaningful error messages and introducing custom exception types for better diagnostics.
    Externalize configuration settings, such as weekend days or constants, to make the solution more configurable without code modification.
    Consider abstracting the database access further by introducing a repository or service layer to encapsulate data access logic.
    Conduct integration testing to ensure the correct interaction between the solution components, such as the database and the application logic.
    Optimize the performance of database queries and algorithms, especially if dealing with large datasets.

Q4. What's a technology that you're excited about and where do you see this 
    being applicable? (Your answer does not have to be related to this problem)

## Request and Response Examples

Please see examples for how to make requests and the expected response below.

### Request

The service is setup as a Web API and takes a request in the following format

~~~~ 
GET /api/DespatchDate?ProductIds={product_id}&orderDate={order_date} 
~~~~

e.g.

~~~~ 
GET /api/DespatchDate?ProductIds=1&orderDate=2018-01-29T00:00:00
GET /api/DespatchDate?ProductIds=2&ProductIds=3&orderDate=2018-01-29T00:00:00 
~~~~

### Response

The response will be a JSON object with a date property set to the resulting 
Despatch Date

~~~~ 
{
    "date" : "2018-01-30T00:00:00"
}
~~~~ 

## Acceptance Criteria

### Lead time added to despatch date  

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 02/01/2018  

**Given** an order contains a product from a supplier with a lead time of 2 days  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Wednesday - 03/01/2018  

### Supplier with longest lead time is used for calculation

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order also contains a product from a different supplier with a lead time of 2 days  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Wednesday - 03/01/2018  

### Lead time is not counted over a weekend

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 08/01/2018  

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Saturday - 06/01/18  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 09/01/2018  

**Given** an order contains a product from a supplier with a lead time of 1 days  
**And** the order is place on a Sunday - 07/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 09/01/2018  

### Lead time over multiple weeks

**Given** an order contains a product from a supplier with a lead time of 6 days  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 15/01/2018  

**Given** an order contains a product from a supplier with a lead time of 11 days  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 22/01/2018
