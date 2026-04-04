import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { EnquiryService } from '@core/services/enquiry.service';

@Component({
  selector: 'ts-enquiry-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './enquiry-modal.component.html',
  styleUrl: './enquiry-modal.component.scss'
})
export class EnquiryModalComponent {
  enquiryService = inject(EnquiryService);
  private fb = inject(FormBuilder);

  submitting = signal(false);
  submitted = signal(false);
  ticketId = signal('');

  services = [
    'Custom Software Development','Web Application Development',
    'Mobile App Development','AI & Machine Learning',
    'Cloud & DevOps','Cybersecurity & Compliance',
    'IT Consulting & Strategy','IT Staff Augmentation'
  ];

  form = this.fb.group({
    fullName: ['', [Validators.required, Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.email]],
    phone: [''],
    company: [''],
    serviceName: ['', Validators.required],
    message: ['', [Validators.required, Validators.minLength(20)]]
  });

  get prefilledService() { return this.enquiryService.prefilledService(); }
  get isOpen() { return this.enquiryService.modalOpen(); }

  close() { this.enquiryService.closeModal(); this.submitted.set(false); this.form.reset(); }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.submitting.set(true);
    const v = this.form.value;
    const service = this.prefilledService;
    this.enquiryService.submit({
      fullName: v.fullName!,
      email: v.email!,
      phone: v.phone || undefined,
      company: v.company || undefined,
      serviceSlug: service?.slug ?? v.serviceName!.toLowerCase().replace(/\s+/g, '-'),
      serviceName: service?.name ?? v.serviceName!,
      message: v.message!,
      sourcePageUrl: window.location.href
    }).subscribe({
      next: (res) => {
        this.ticketId.set(res.ticketId);
        this.submitted.set(true);
        this.submitting.set(false);
      },
      error: () => this.submitting.set(false)
    });
  }
}
