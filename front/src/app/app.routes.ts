import { Routes } from '@angular/router';

export const routes: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', loadComponent: () => import('./components/home/home').then(m => m.Home)},
    {path: 'accounts', loadComponent: () => import('./components/accounts/accounts').then(m => m.Accounts)},
    {path: 'transactions', loadComponent: () => import('./components/transactions/transactions').then(m => m.Transactions)},
];
