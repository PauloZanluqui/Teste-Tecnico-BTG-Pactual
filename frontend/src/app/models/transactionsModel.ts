export interface TransactionModel {
    transactionId: string;
    accountDocumentNumber: string;
    transactionValue: number;
    transactionDate: Date;
}

export interface CreateTransactionModel {
    accountDocumentNumber: string;
    transactionValue: number;
}