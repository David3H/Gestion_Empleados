import { Injectable, inject } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

export enum NotificationType {
  Success = 'green-success-snackbar',
  Error = 'red-error-snackbar',
  Info = 'blue-info-snackbar'
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

   private snackBar = inject(MatSnackBar);

  private show(message: string, panelClass: string, duration = 3000) {
    const config: MatSnackBarConfig = {
      duration,
      panelClass: [panelClass],
      horizontalPosition: 'right',
      verticalPosition: 'top'
    };
    this.snackBar.open(message, 'Cerrar', config);
  }

  success(msg: string) {
    this.show(msg, NotificationType.Success);
  }

  error(msg: string) {
    this.show(msg, NotificationType.Error, 5000);
  }

  info(msg: string) {
    this.show(msg, NotificationType.Info);
  }
}
