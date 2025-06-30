import { Routes } from '@angular/router';

export const routes: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)},
    {path: 'accounts', loadComponent: () => import('./components/accounts/accounts.component').then(m => m.AccountsComponent)},
    {path: 'transactions', loadComponent: () => import('./components/transactions/transactions.component').then(m => m.TransactionsComponent)},
];
