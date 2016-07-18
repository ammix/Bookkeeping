
Use Bookkeeping

SELECT trs.TransactionDate, cps.Name, trs.Amount, trs.Note, acs.Name, acs.Currency
FROM Transactions trs
INNER JOIN CounterParties cps
ON trs.CounterpartyId = cps.Id
INNER JOIN Accounts acs
ON trs.AccountId = acs.Id



SELECT trs.TransactionDate, cps.Name, ars.Label, trs.Amount, ils.Price, CONCAT(trs.Note, ils.Note) AS Note, acs.Name, acs.Amount, acs.Currency
	FROM Transactions trs INNER JOIN CounterParties cps ON trs.CounterpartyId = cps.Id
						  INNER JOIN Accounts acs ON trs.AccountId = acs.Id
						  LEFT OUTER JOIN InvoiceLines ils ON trs.Id = ils.TransactionId
						  LEFT OUTER JOIN Articles ars ON ars.Id = ils.ArticleId