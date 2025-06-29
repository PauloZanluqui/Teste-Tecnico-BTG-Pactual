import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accounts',
  standalone: true,
  templateUrl: './accounts.html',
  styleUrls: ['./accounts.css']
})
export class Accounts implements OnInit {
  accounts: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any[]>('http://localhost:5201/account') // ajuste a URL se necessário
      .subscribe({
        // next: (data) => this.accounts = data,
        next: (data) => console.log(data),
        error: (err) => console.error('Erro ao carregar contas', err)
      });
  }
}
