import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumber } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ApiService } from '../../../services/api.service';
import { CreateTransactionModel } from '../../../models/transactionsModel';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-create-transaction-modal',
    imports: [FormsModule, ReactiveFormsModule, InputNumber, Dialog, InputTextModule, InputMaskModule, ButtonModule],
    templateUrl: './create-transaction-modal.component.html',
    styleUrl: './create-transaction-modal.component.css'
})
export class CreateTransactionModalComponent implements OnInit {
    @Input() showModal: boolean = false;
    @Output() hideModal = new EventEmitter<void>();

    transactionForm: FormGroup = new FormGroup({
        accountDocumentNumber: new FormControl<string | null>(null),
        transactionValue: new FormControl<number | null>(null),
    });

    constructor(private api: ApiService) { }

    async ngOnInit() {
        this.transactionForm.reset();
    }

    async createTransaction() {
        if (this.transactionForm.invalid) return;

        const transaction: CreateTransactionModel = this.transactionForm.value;

        try {
            await this.api.post('/transaction', transaction);
            this.closeModal();
        } catch (error) {
            if (error instanceof HttpErrorResponse) {
                alert('Erro ao salvar conta: ' + error.error);
            } else {
                alert('Erro ao salvar conta');
            }
        }
    }

    closeModal() {
        this.hideModal.emit();
        this.transactionForm.reset();
    }
}
