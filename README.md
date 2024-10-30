> **Note** This repository is developed using .netstandard2.0.

| Name     | Details |
|----------|----------|
| LinkShortener | [![NuGet Version](https://img.shields.io/nuget/v/LinkShortener.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/LinkShortener/) [![Nuget Downloads](https://img.shields.io/nuget/dt/LinkShortener.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/LinkShortener)|

This repository represents an implementation of transforming long, ugly links into more nice, memorable, and trackable short URLs. The current code implementation allows to the shortening of long links and storing them.

The `shorten` process represents a generation of custom length code (identifier). This code is unique and can't find another record with the same code.

**Here you can find the bases functionalities like:**

- Generate new URL unique code;
- Store into database related information;
- Check unique code;
- Disable any previously generated code;
- Find and return hidden URL by their code;

The store creation and query related was developed using ADO.NET with a few validation of db provider for store fields data type (`Npgsql` and `Mysql`).

To understand more efficiently how you can use available functionalities please consult the [using documentation/file](docs/usage.md).

**In case you wish to use it in your project, u can install the package from <a href="https://www.nuget.org/packages/LinkShortener" target="_blank">nuget.org</a>** or specify what version you want:

> `Install-Package LinkShortener -Version x.x.x.x`

## Content
1. [USING](docs/usage.md)
1. [CHANGELOG](docs/CHANGELOG.md)
1. [BRANCH-GUIDE](docs/branch-guide.md)