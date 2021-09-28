# Developers' Guide

## Workstation Setup
Helpful guide for getting started with development.

### Tools
- Git Bash (Windows) 
- Visual Studio 2019
- MySQL Database
- Python 3.8 or 3.9

Git Bash and Visual Studio are simple download and installs. Installation of C# project dependencies will be asked by Visual Studio upon opening of the solution file (*.sln). 

For MySQL this guide assumes you have installed it and set up a root account or user with necessary access (since this is a local development environment, the security of an account is not super significant). You will need to update the connection string in the appsettings.Development.json file. Mostly likily, the connection string will require renaming the server to `localhost` and filling in the username and password. Since you do not want your database password to be public, the Developing appsettings is excluded for git using the .gitignore file. This can be overruled if some other configuration needs to be added, but remember to put the dummy authenication variables back.

Python 3.7 has a security issue that was fixed with 3.8, so 3.8 or 3.9 is recommended if you are installing a new python version, but any version of python 3 will work. Python is used for testing and creating the development database. It is recommended that you create a virtual environment for this project (using whatever method you are comfortable, i.e. venv, poetry, or conda).

### Development Database
Once you have MySQL and Python installed and your connection string updated, you can run the python script `database/db_setup.py` to create the development database.