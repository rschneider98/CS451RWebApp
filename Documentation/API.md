# API Documentation
Most of the AI endpoints use HTTP POST method as to prevent inclusion of sensitive information in the URL. Authentication and usage is explained in their sections.

All Inputs are expected to be JSON and all outputs are JSON objects.

## Endpoints
- `getAccount` **[POST]**
    - Parameters
        - int `ID`: The Account ID to get more information for
    - Returns: Information about a account (email is masked)
    ```
    {
        "accountID": int,
        "accountType": string,
        "currentBalanceDollars": int,
        "currentBalanceCents": int,
        "users": [
            {"userID": int,
            "email": string,
            "firstName": string,
            "lastName": string}, ...
        ]
    }
    ```
- `getAccount` **[POST]**
    - Parameters
        - int `Token`: The Account Token (generated from a user lookup) to get more information for
    - Returns: Information about a account (email and account number is masked)
    ```
    {
        "accountID": string,
        "accountType": string,
        "currentBalanceDollars": int,
        "currentBalanceCents": int,
        "users": [
            {"userID": int,
            "email": string,
            "firstName": string,
            "lastName": string}, ...
        ]
    }
    ```
- `getAccountReport` **[POST]**
    - Parameters
        - int `Token`: The Account Token (generated from a user lookup) to get more information for
    - Returns: Summary information about a account transactions over past time period
    ```
    {
        "accountID": int,
        "accountType": string,
        "currentBalanceDollars": int,
        "currentBalanceCents": int,
        "previousBalanceDollars": int,
        "previousBalanceCents": int,
        "categories": [
            {"category": string,
            "amountDollars": int,
            "amountCents": int,
            "vendors": [
                {"vendor": string,
                "amountDollars": int,
                "amountCents": int}, ...
            ]}, ...
        ]
    }
    ```
- `getTransaction` **[POST]**
    - Parameters
        - int `ID`: The Transaction ID to get more information for
    - Returns: A transaction
    ```
    {
        "transactionID": int, 
        "timeMonth": int, 
        "timeDay": int, 
        "timeYear": int, 
        "amountDollars": int, 
        "amountCents": int, 
        "endBalanceDollars": int, 
        "endBalanceCents": int, 
        "vendor": string,
        "vendorDescription": string,
        "vendorCategory": string
    }
    ```
- `getTransactionHistory` **[POST]**
    - Parameters
        - int `ID`: The Account ID to get transaction history for
        - int `PageSize`: The number of results to return per page
        - int `PageNumber` (default = 0): The page of results you want to view
    - Returns: List of transactions from newest to oldest
    ```
    {
        "Transactions": [
            {"transactionID": int, 
            "timeMonth": int, 
            "timeDay": int, 
            "timeYear": int, 
            "amountDollars": int, 
            "amountCents": int, 
            "endBalanceDollars": int, 
            "endBalanceCents": int, 
            "vendor": string}, ...
        ]
    } 
    ```
- `getTransactionHistory` **[POST]**
    - Parameters
        - int `ID`: The Account token to get transaction history for
        - int `PageSize`: The number of results to return per page
        - int `PageNumber` (default = 0): The page of results you want to view
    - Returns: List of transactions from newest to oldest
    ```
    {
        "Transactions": [
            {"transactionID": int, 
            "timeMonth": int, 
            "timeDay": int, 
            "timeYear": int, 
            "amountDollars": int, 
            "amountCents": int, 
            "endBalanceDollars": int, 
            "endBalanceCents": int, 
            "vendor": string}, ...
        ]
    } 
    ```
- `getUser` **[POST]**
    - Parameters
        - int `ID`: The User ID to get more information for
    - Returns: Information about a user (with masked email and account numbers), and the account token can be used to get account information
    ```
    {
        "userID": int,
        "email": string,
        "firstName": string,
        "lastName": string,
        "accounts": [
            {"accountID": string,
            "accountType": string,
            "currentBalanceDollars": int,
            "currentBalanceCents": int,
            "token": int}, ...
        ]
    }
    ```
- `getUserPrivileged` **[POST]**
    - Parameters
        - int `ID`: The User ID to get more information for
    - Returns: Information about a user (with sensitive information and no masking)
    ```
    {
        "userID": int,
        "email": string,
        "firstName": string,
        "lastName": string,
        "SSN": int,
        "addressLine1" string,
        "addressLine2" string,
        "city" string,
        "postalState" string,
        "accounts": [
            {"accountID": string,
            "accountType": string,
            "currentBalanceDollars": int,
            "currentBalanceCents": int,
            "token": int}, ...
        ]
    }
    ```
