Use Bookkeeping

SELECT
trs.TransactionDate AS Date,
cps.Name AS Counterparty,
ars.Label AS Article,
trs.Amount,
ils.Price,
CONCAT(trs.Note, ils.Note) AS Note,
acs.Name AS Acount,
sns.Amount AS Balance,
acs.Currency

FROM Transactions trs 
	LEFT OUTER JOIN InvoiceLines ils ON trs.Id = ils.TransactionId
	LEFT OUTER JOIN Articles ars ON ars.Id = ils.ArticleId
	INNER JOIN CounterParties cps ON trs.CounterpartyId = cps.Id
	INNER JOIN Accounts acs ON trs.AccountId = acs.Id
	INNER JOIN Snapshots sns ON sns.AccountId = trs.AccountId
WHERE DATEPART(month, trs.TransactionDate)=6 AND DATEPART(day, sns.SnapshotDate)=1