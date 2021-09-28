CREATE TABLE account (
    AccountID int unsigned NOT NULL AUTO_INCREMENT PRIMARY KEY,
    AccountType int unsigned NOT NULL,
    UserID1 int unsigned NOT NULL,
    UserID2 int unsigned NULL,
    UserID3 int unsigned NULL,
    UserID4 int unsigned NULL,
    UserID5 int unsigned NULL,
    CONSTRAINT FK_AccountType FOREIGN KEY (AccountType) REFERENCES account_type(AccountType),
    CONSTRAINT FK_User1 FOREIGN KEY (UserID1) REFERENCES user(UserID),
    CONSTRAINT FK_User2 FOREIGN KEY (UserID2) REFERENCES user(UserID),
    CONSTRAINT FK_User3 FOREIGN KEY (UserID3) REFERENCES user(UserID),
    CONSTRAINT FK_User4 FOREIGN KEY (UserID4) REFERENCES user(UserID),
    CONSTRAINT FK_User5 FOREIGN KEY (UserID5) REFERENCES user(UserID)
);