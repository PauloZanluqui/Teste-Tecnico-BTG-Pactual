import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TransactionModel } from '../../models/transactionsModel';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { CreateTransactionModalComponent } from "./create-transaction-modal/create-transaction-modal.component";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-transactions',
    imports: [CommonModule, TableModule, ButtonModule,  InputTextModule, InputMaskModule, CreateTransactionModalComponent],
    templateUrl: './transactions.component.html',
    styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit {
    isLoading: boolean = true;
    modalVisible: boolean = false;
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
            if (error instanceof HttpErrorResponse) {
                alert('Erro ao buscar transações: ' + error.error);
            } else {
                alert('Erro ao buscar transações');
            }
        }
        this.isLoading = false;
    }

    showModal() {
        this.modalVisible = true;
    }

    hideModal() {
        this.refreshTransactions();
        this.modalVisible = false;
    }
}
