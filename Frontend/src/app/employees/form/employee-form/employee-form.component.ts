import { Component, inject, OnInit  } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { NotificationService } from '../../../core/notifications/notification.service';
import { EmployeeService } from '../../services/employee.service';
import { Router, ActivatedRoute  } from '@angular/router';
import { EmployeeDTO } from '../../dto/employee.dto';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employee-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent implements OnInit {
  fb = inject(FormBuilder);
  router = inject(Router);
  route = inject(ActivatedRoute);
  notif = inject(NotificationService);
private service = inject(EmployeeService);
isEdit = false;
form = this.fb.group({
  id: [0],
  firstName: ['', Validators.required],
  lastName: ['', Validators.required],
  email: ['', [Validators.required, Validators.email]],
  position: ['', Validators.required],
  hireDate: ['', Validators.required],
  active: [true],
  storeId: ['', Validators.required],
});

 ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
if (idParam) {
    this.isEdit = true;
    const id = +idParam;
    this.service.get(id).subscribe({
      next: (e: EmployeeDTO) => {
        const iso = e.hireDate;

        const formatted = this.formatAsDateInput(iso);

        this.form.patchValue({
          ...e,
          hireDate: formatted,
          active: e.active
        });
      },
      error: (err: HttpErrorResponse) => this.notif.error(err.error.message || err.message)
    });
  }
  }

 onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const data = this.form.value as EmployeeDTO;
    const action$ = this.isEdit ? this.service.update(data.id!, data) : this.service.create(data);
    action$.subscribe({
      next: () => {
        this.notif.success(`Empleado ${ this.isEdit ? 'actualizado corretamente' : 'creado correctamente' }`);
        this.router.navigate(['/']);
      },
      error: (err: HttpErrorResponse) => this.notif.error(err.error.message || err.message)
    });
  }

  cancel() {
    this.router.navigate(['/']);
  }

  private formatAsDateInput(iso: string): string {
  if (!iso) return '';
  const d = new Date(iso);
  const year = d.getFullYear();
  let month = (d.getMonth() + 1).toString().padStart(2, '0');
  let day = d.getDate().toString().padStart(2, '0');
  return `${year}-${month}-${day}`;
}
}
