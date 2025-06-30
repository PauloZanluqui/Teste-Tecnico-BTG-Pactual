import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TransactionModel } from '../../models/transactionsModel';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-transactions',
    imports: [CommonModule, TableModule, ButtonModule],
    templateUrl: './transactions.component.html',
    styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit {
    isLoading: boolean = true;
    transactions: TransactionModel[] = [];

    constructor(private api: ApiService) { }

    async ngOnInit() {
        await this.refreshTransactions();
    }

    async refreshTransactions() {
        this.isLoading = true;
        try {
            this.transactions = await this.api.get<TransactionModel[]>('/transaction');
        } catch (error) {
            console.error('Erro ao atualizar transações:', error);
        }
        this.isLoading = false;
    }
}
