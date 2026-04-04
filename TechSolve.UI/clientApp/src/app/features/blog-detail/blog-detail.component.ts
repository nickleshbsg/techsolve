import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { ApiService } from '@core/services/api.service';
import { EnquiryService } from '@core/services/enquiry.service';
import { BlogDetail } from '@core/models/blog.model';

@Component({
  selector: 'ts-blog-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, DatePipe],
  templateUrl: './blog-detail.component.html',
  styleUrl: './blog-detail.component.scss'
})
export class BlogDetailComponent implements OnInit {
  private api = inject(ApiService);
  private route = inject(ActivatedRoute);
  enquiry = inject(EnquiryService);

  post = signal<BlogDetail | null>(null);
  loading = signal(true);

  catColor: Record<string, string> = {
    'AI & Machine Learning': '#818cf8',
    'Angular & .NET Core': '#1a3ed4',
    'Compliance': '#0f766e',
    'Cloud': '#0ea5e9',
    'Cybersecurity': '#dc2626'
  };

  ngOnInit() {
    const slug = this.route.snapshot.paramMap.get('slug') ?? '';
    this.api.getBlogBySlug(slug).subscribe({
      next: p => { this.post.set(p); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
