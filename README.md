# FudGPortal
## Food-ordering-system

An ASP.NET web application for ordering food online. It has three views
1) User view          -> an user will only get access to user view and user functionalities.
2) Restaurant view    -> a restaurant only will get access to restaurant view and restaurant functionalities.
3) Admin view         -> an admin will only get access to admin view and admin functionalities.

User can register, login, select their favorite restaurant and can place their order. An Restaurant can register, login, add food items and update food items.
An admin will manage customer and restaurant.

### Architecture

1) FudGPortal              -> ASP.NET MVC project
2) FudGWebApi.Data         -> Database Context
3) FudGWebApi.IService     -> generic repository class
4) FudGWebApi.Service      -> all business logic, interfaces and data access operation
5) FudGWebApi.Models       -> entities and view model
6) FudGWebApi.Controllers  ->  all controllers.
7) FudGJquery.Views        ->  all views
