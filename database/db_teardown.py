import os
# mysql connector library
import mysql
# sqlalchemy is used to connect to db
import sqlalchemy as sql
from sqlalchemy.sql import text
from sqlalchemy.exc import IntegrityError

# SET ENVIRONMENT VARIABLES FOR DB
# since this will be run at the same location as DB these are formatted for local connections to DB
#host = "127.0.0.1"  # localhost
host = "raspberrypi"
port = 3306         # local port
# DO NOT hard-code this
dbuser = os.environ.get("DB_USER")
pwd = os.environ.get("DB_PWD") 

# create SQL engine for DB
engine = sql.create_engine(f'mysql+mysqlconnector://{dbuser}:{pwd}@{host}:{port}')

with open('sql/delete_db.sql', mode='r') as f:
    f_text = f.read()
query = text(f_text)
try:
    with engine.connect() as connection:
        connection.execute(query)
except IntegrityError:
    print("Failed to delette database")


