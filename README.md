# Kitara Kauppa (The Guitar Shop): E-commerce App

<!-- ![TypeScript](https://img.shields.io/badge/TypeScript-green)
![SASS](https://img.shields.io/badge/SASS-hotpink)
![React](https://img.shields.io/badge/React-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-brown) -->

### Backend

![.NET Core](https://img.shields.io/badge/.NET%20Core-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-blue)
![xUnit](https://img.shields.io/badge/xUnit-black)
![Swagger](https://img.shields.io/badge/-Swagger-%2523Clojure)
![Auto mapper](https://img.shields.io/badge/automapper-red)

### Frontend

![React](https://img.shields.io/badge/React-blue)
![Redux toolkit](https://img.shields.io/badge/RTK-purple)
![TypeScript](https://img.shields.io/badge/TypeScript-green)
![MUI](https://img.shields.io/badge/Material%20UI-007FFF)
![Jest](https://img.shields.io/badge/Jest-323330)
![React Router](https://img.shields.io/badge/-React%20Router-CA4245)
![React Forms Hook](https://img.shields.io/badge/react--hook--form-EC5990)

Kitara Kauppa, is an online guitar store which guitar enthusiastic can buy their dream guitars at a very reasonable price in Finland.

API Documentation URL : https://kitarakauppa.azurewebsites.net/swagger

Frontend App URL : https://main.d7b5s8hshsh29.amplifyapp.com

## Installation

1. Clone the project
2. Set the db connection string in appsetting.json
3. `dotnet run` to run the project

## Features

### Customer Specific

- List latest available guitars (as anonymous user)
- Check more details on each guitar (as anonymous user)
- Add / Remove guitars and accessories to / from the cart (as registered user)
- Purchase the orders (as registered user)
- Check the status of the order (as registered user)
- Have a review with your experience (as registered user)

### Admin Specific

- Add / remove users of the online store (Staff members)
- Make users active / inactive just in case
- Add new guitars and accessories or remove outdated or unavailable items
- Define / remove or update guitars and accessories categories
- Update the status of the orders
- Possibility of deleting reviews just in case

### Credentials

#### Admin

Username: admin@kk.com
<br />
Password: Abc123@@@

#### Customer

Username: sanuja@gmail.com
<br/>
Password: Abc123@@@

## Sample Screenshot

![Screenshot](./Backend/screenshots/sample_screenshot.jpg)

## Architecture

### Technology Stack

![Tech Stack](./Backend/screenshots/techstack.png)

### ER Diagram

![ER Diagram](./Backend/diagrams/KitaraKauppa-ER-Diagram_v1_6.png)

### Demo Environment Architecture

![Demo Environment Architecture](./Backend/screenshots/demo%20architecture.png)

### Production Application Architecture

![Production Environment Architecture](./Backend/screenshots/productionenv.png)

### Dataflow Management

![Dataflow Management](./Backend/screenshots/dfd.png)
