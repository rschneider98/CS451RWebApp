SELECT TransactionID, TimeMonth, TimeDay, TimeYear, AmountDollars, AmountCents, EndBalanceDollars, EndBalanceCents, Vendor
FROM transactions
WHERE AccountID = @ID;