import { Component, OnInit } from '@angular/core';
import { AccountModel } from '../../models/accountsModel';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputMaskModule } from 'primeng/inputmask';
import {
    FormControl,
    FormGroup,
    FormsModule,
    ReactiveFormsModule,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumber } from 'primeng/inputnumber';
import { Tooltip } from 'primeng/tooltip';

@Component({
    selector: 'app-accounts',
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        TableModule,
        ButtonModule,
        Dialog,
        InputMaskModule,
        InputTextModule,
        InputNumber,
        Tooltip,
    ],
    templateUrl: './accounts.component.html',
    styleUrl: './accounts.component.css',
})
export class AccountsComponent implements OnInit {
    accountForm: FormGroup = new FormGroup({
        documentNumber: new FormControl<string | null>(null),
        agency: new FormControl<string | null>(null),
        accountNumber: new FormControl<string | null>(null),
        availableLimit: new FormControl<number | null>(null),
    });
    accounts: AccountModel[] = [];
    isLoading: boolean = true;
    isEditing: boolean = false;
    modalVisible: boolean = false;

    constructor(private api: ApiService) { }

    async ngOnInit() {
        await this.refreshAccounts();
    }

    async refreshAccounts() {
        this.isLoading = true;
        try {
            this.accounts = await this.api.get<AccountModel[]>('/account');
        } catch (error) {
            console.error('Erro ao atualizar contas:', error);
        }
        this.isLoading = false;
    }

    showModal(isEditing: boolean = false, account?: AccountModel) {
        this.isEditing = isEditing;
        if (isEditing && account) {
            this.accountForm.setValue({
                documentNumber: account.documentNumber,
                agency: account.agency,
                accountNumber: account.accountNumber,
                availableLimit: account.availableLimit,
            });
        } else {
            this.accountForm.reset();
        }
        this.modalVisible = true;
    }

    hideModal() {
        this.modalVisible = false;
        this.isEditing = false;
    }

    async createAccount() {
        if (this.accountForm.invalid) return;

        const account: AccountModel = this.accountForm.value;

        try {
            this.isLoading = true;
            await this.api.post('/account', account);
            await this.refreshAccounts();
            this.hideModal();
        } catch (error) {
            console.error('Erro ao salvar conta:', error);
        } finally {
            this.isLoading = false;
        }
    }

    async changeLimit() {
        const account: AccountModel = this.accountForm.value;

        if (account.documentNumber == null) return;

        try {
            this.isLoading = true;
            await this.api.patch(`/account/changelimit/${account.documentNumber}`, {
                newLimit: account.availableLimit,
            });
            await this.refreshAccounts();
        } catch (error) {
            console.error('Erro ao alterar limite:', error);
        } finally {
            this.isLoading = false;
            this.hideModal();
        }
    }

    async deleteAccount(documentNumber: string) {
        try {
            this.isLoading = true;
            await this.api.delete(`/account/${documentNumber}`);
            await this.refreshAccounts();
        } catch (error) {
            console.error('Erro ao excluir conta:', error);
        } finally {
            this.isLoading = false;
        }
    }
}
