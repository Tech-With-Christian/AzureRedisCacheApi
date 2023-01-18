<a name="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://blog.christian-schou/">
    <img src="images/logo.png" alt="Christian Schou Logo" width="75" height="75">
  </a>

  <h3 align="center">.NET Azure Redis Cache Demo</h3>

  <p align="center">
    A simple demo of how you can implement Azure Redis Cache in a .NET Core Web API with the repository pattern.
    <br />
    <br />
    <a href="https://github.com/Tech-With-Christian/AzureRedisCacheApi/issues">Report a Bug or Request a Feature</a>
    ·
    <a href="https://blog.christian-schou.dk/how-to-implement-azure-redis-cache-in-net-core-web-api">Read the full in-depth article about this project</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Redis API Service</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About the API Service

[![Swagger Implementation][product-screenshot]](https://blog.christian-schou.dk/how-to-implement-azure-redis-cache-in-net-core-web-api)

This is a simple API project to demonstrate how you can implement a distributed Redis cache hosted at Azure into your .NET Core Web API. You will be able to replicate the logic using either .NET 6 or .NET 7.

Why should you use a cache like Redis?
* Response time is reduced drastically
* Your database will be able to breath and serve requests other than just GET operations
* Your clients will be happy :smile:

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

The API demo is built using the following technologies and tools.
* Visual Studio 2022
* .NET 7
* Azure Cache for Redis
* Microsoft SQL Server
* Entity Framework Core
* C#

<!-- GETTING STARTED -->
## Getting Started

To get started developing further on the Azure Redis API service, you have to clone the project to your local computer.

### Prerequisites

* Visual Studio 2022
* .NET 7 SDK
* Local installation of Microsoft SQL Server (Express, Developer, etc...)
* An Azure Account with an Active Subscription

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

[product-screenshot]: images/screenshot.png
