# MovieSearchApi
This api allows user to search movie based on different search criteria

# Description of the problem and solution

Create a service that lists movies that have been filmed in San Francisco. Users should be able to
filter movies by title, release, location, distributor, director writer and actors.
The data is available on https://data.sfgov.org/Culture-and-Recreation/Film-Locations-in-San-Francisco/yitu-d5am

# Whether the solution focuses on backend, frontend or if it’s full stack

This solution is a backend api developed using .net core 2.1 and entity framework. sqllite DB has been used as repository.

# Reasoning behind your technical choices, including architectural

.net core has been chosen as framework because this can targetted to be hosted at low-cost app service plan of any cloud provider across platform. sqllite DB has been used due to time/resource constraint in dev environment. Considering the data volume of this application sqllite is good enough to demonstrate this as POC.

# Trade-offs you might have made, anything you left out, or what you might do differently if
# you were to spend additional time on the project.

I would have gone for MongoDB as data store.

I would have moved the data access logic to a repository layer and used repository pattern and unit test controller/business logic.

Would have added unit test project.

