import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../employees/services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../core/notifications/notification.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  fb = inject(FormBuilder);
  auth = inject(AuthService);
  router = inject(Router);
  notif = inject(NotificationService);

  form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

 ngOnInit() {
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/']);
    }
  }

   onSubmit() {
    if (this.form.invalid) return;
    const { username, password } = this.form.value;
    this.auth.login(username!, password!).subscribe({
      next: () => {
        this.notif.success('Inicio de sesión exitoso');
        this.router.navigate(['/']);
      },
      error: () => this.notif.error('Credenciales inválidas'),
    });
  }

}
