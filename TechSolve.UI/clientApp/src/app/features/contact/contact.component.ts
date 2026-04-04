import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { WhatsAppService } from '@core/services/whatsapp.service';
import { EnquiryService } from '@core/services/enquiry.service';

@Component({
  selector: 'ts-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent {
  private wa = inject(WhatsAppService);
  private enquiry = inject(EnquiryService);
  private fb = inject(FormBuilder);

  submitting = signal(false);
  submitted = signal(false);
  ticketId = signal('');

  services = [
    'Custom Software Development', 'Web Application Development',
    'Mobile App Development', 'AI & Machine Learning',
    'Cloud & DevOps', 'Cybersecurity & Compliance',
    'IT Consulting & Strategy', 'IT Staff Augmentation', 'Other'
  ];

  form = this.fb.group({
    fullName:    ['', [Validators.required, Validators.maxLength(100)]],
    email:       ['', [Validators.required, Validators.email]],
    phone:       [''],
    company:     [''],
    serviceName: ['', Validators.required],
    budget:      [''],
    timeline:    [''],
    message:     ['', [Validators.required, Validators.minLength(20)]]
  });

  openWhatsApp() { this.wa.openChat('contact-page'); }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.submitting.set(true);
    const v = this.form.value;
    this.enquiry.submit({
      fullName: v.fullName!,
      email: v.email!,
      phone: v.phone || undefined,
      company: v.company || undefined,
      serviceSlug: v.serviceName!.toLowerCase().replace(/\s+&?\s*/g, '-').replace(/[^a-z0-9-]/g, ''),
      serviceName: v.serviceName!,
      message: `${v.message}\n\nBudget: ${v.budget || 'Not specified'}\nTimeline: ${v.timeline || 'Not specified'}`,
      sourcePageUrl: window.location.href
    }).subscribe({
      next: (res) => { this.ticketId.set(res.ticketId); this.submitted.set(true); this.submitting.set(false); },
      error: () => this.submitting.set(false)
    });
  }
}
