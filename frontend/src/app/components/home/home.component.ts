import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CreateTransactionModalComponent } from '../transactions/create-transaction-modal/create-transaction-modal.component';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-home',
    imports: [CommonModule, ButtonModule, RouterLink, CreateTransactionModalComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent {
    modalVisible: boolean = false;

    showModal() {
        this.modalVisible = true;
    }

    hideModal() {
        this.modalVisible = false;
    }
}
