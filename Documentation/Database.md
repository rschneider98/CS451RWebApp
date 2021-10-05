# Schmea Definition for Relational Database

## USER
Identify users of our services.

| Field | Type | Other |
| -- | -- | -- |
| Email | string(320) | Primary Key |
| UserID | unsigned int(255) | Indexed |
| FirstName | string(50) | NA |
| LastName | string(50) | NA |
| Salt | BINARY(20) | NA |
| Password | BLOB(512) | NA |

## SPI
Tie our user information to protected information. This is a separate table to restrict unnecessary access.

| Field | Type | Other |
| -- | -- | -- |
| UserID | unsigned int(255) | Primary Key |
| SSN | unsigned int(255) | NA |

## NOTIFICATION
This would define the types of notification rules users can select from, describe the rule for the user, and provide a function name that actually implements the queries.

| Field | Type | Other |
| -- | -- | -- |
| FnName | string(64) | Primary Key |
| Name | string(64) | NA |
| Description | string(256) | NA |


## USER_NOTIFICATION
This should include entries to enable notification functions for users.

| Field | Type | Other |
| -- | -- | -- |
| UserID | unsigned int(255) | Primary Key |
| FnName | string(64) | NA |


## ACCOUNT
This would tie our users to the accounts that they have ownership of.

| Field | Type | Other |
| -- | -- | -- |
| AccountID | unsigned int(255) | Primary Key |
| Type | string(320) | Enumerated Account Types |
| UserID1 | unsigned int(255) | Indexed |
| UserID2 | unsigned int(255) | Indexed |
| UserID3 | unsigned int(255) | Indexed |
| UserID4 | unsigned int(255) | Indexed |
| UserID5 | unsigned int(255) | Indexed |

Possible account types:
1. Checking
2. Savings

Business Rule: Only a maximum of five people can have access to the same account.

## TRANSACTIONS
This would tie accounts to indivdual transactions

| Field | Type | Other |
| -- | -- | -- |
| TransactionID | unsigned int(255) | Primary Key |
| AccountID | unsigned int(255) | Foreign Key |
| Year | unsigned smallint(255) | NA |
| Month | unsigned smallint(255) | NA |
| Timestamp | VARCHAR(25) | NA |
| Amount | DECIMAL(16, 2) | NA |
| LocationAddress | VARCHAR(50) | NA |
| LocationCity | VARCHAR(50) | NA |
| LocationStCd | VARCHAR(2) | NA |
| LocationCtnyCd | VARCHAR(5) | NA |
| Vendor | VARCHAR(100) | Foreign Key |

## CATEGORY
List and description of transaction categories

| Field | Type | Other |
| -- | -- | -- |
| Category | unsigned int(255) | Primary Key |
| Description | VARCHAR(255) | NA |

## VENDOR_CAT
Tie vendors to transaction categories

| Field | Type | Other |
| -- | -- | -- |
| Vendor | VARCHAR(100) | Primary Key |
| Category | unsigned int(255) | NA |