import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApiService } from '@core/services/api.service';
import { BlogSummary } from '@core/models/blog.model';

@Component({
  selector: 'ts-blog',
  standalone: true,
  imports: [CommonModule, RouterLink, DatePipe],
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.scss'
})
export class BlogComponent implements OnInit {
  private api = inject(ApiService);
  posts = signal<BlogSummary[]>([]);
  total = signal(0);
  activeCategory = signal<string | undefined>(undefined);

  catColor: Record<string, string> = {
    'AI & Machine Learning': '#818cf8',
    'Angular & .NET Core': '#1a3ed4',
    'Compliance': '#0f766e',
    'Cloud': '#0ea5e9',
    'Cybersecurity': '#dc2626',
    'Mobile': '#7c3aed'
  };

  categories = [
    { label: 'All', value: undefined as string | undefined },
    { label: 'AI & Machine Learning', value: 'AI & Machine Learning' },
    { label: 'Angular & .NET Core', value: 'Angular & .NET Core' },
    { label: 'Cloud', value: 'Cloud' },
    { label: 'Compliance', value: 'Compliance' },
    { label: 'Cybersecurity', value: 'Cybersecurity' }
  ];

  svgFor(cat: string): string {
    const map: Record<string, string> = {
      'AI & Machine Learning': `<svg viewBox="0 0 240 160" fill="none"><rect width="240" height="160" fill="#eef2ff"/><circle cx="60" cy="80" r="20" fill="#818cf8" opacity=".3"/><circle cx="120" cy="55" r="24" fill="#1a3ed4" opacity=".7"/><circle cx="180" cy="80" r="20" fill="#818cf8" opacity=".3"/><line x1="80" y1="76" x2="97" y2="63" stroke="#1a3ed4" stroke-width="2"/><line x1="143" y1="63" x2="160" y2="76" stroke="#1a3ed4" stroke-width="2"/><circle cx="120" cy="55" r="8" fill="white"/><text x="84" y="130" fill="#1a3ed4" font-size="11" font-weight="700" font-family="sans-serif">AI &amp; Machine Learning</text></svg>`,
      'Angular & .NET Core': `<svg viewBox="0 0 240 160" fill="none"><rect width="240" height="160" fill="#eef2ff"/><polygon points="120,20 185,58 185,118 120,140 55,118 55,58" fill="none" stroke="#1a3ed4" stroke-width="2" opacity=".35"/><polygon points="120,38 168,64 168,110 120,126 72,110 72,64" fill="#1a3ed4" opacity=".12"/><text x="104" y="94" fill="#1a3ed4" font-size="16" font-weight="900" font-family="sans-serif">ng</text><text x="80" y="132" fill="#1a3ed4" font-size="10" font-weight="700" font-family="sans-serif">Angular 19 + .NET 9</text></svg>`,
      'Compliance': `<svg viewBox="0 0 240 160" fill="none"><rect width="240" height="160" fill="#f0fdf4"/><rect x="72" y="24" width="96" height="112" rx="10" fill="white" stroke="#bbf7d0" stroke-width="2"/><rect x="86" y="44" width="68" height="7" rx="3.5" fill="#16a34a" opacity=".6"/><rect x="86" y="58" width="48" height="5" rx="2.5" fill="#bbf7d0"/><rect x="86" y="70" width="58" height="5" rx="2.5" fill="#bbf7d0"/><rect x="86" y="82" width="42" height="5" rx="2.5" fill="#bbf7d0"/><circle cx="120" cy="112" r="14" fill="#dcfce7"/><path d="M113 112l5 5 9-9" stroke="#16a34a" stroke-width="2.5" fill="none" stroke-linecap="round"/></svg>`,
      'Cloud': `<svg viewBox="0 0 240 160" fill="none"><rect width="240" height="160" fill="#f0f9ff"/><ellipse cx="120" cy="72" rx="70" ry="40" fill="white" stroke="#bae6fd" stroke-width="2"/><ellipse cx="80" cy="86" rx="38" ry="24" fill="white" stroke="#bae6fd" stroke-width="2"/><ellipse cx="162" cy="86" rx="38" ry="24" fill="white" stroke="#bae6fd" stroke-width="2"/><text x="106" y="80" font-size="22">☁️</text><text x="85" y="130" fill="#0ea5e9" font-size="10" font-weight="700" font-family="sans-serif">Azure · AWS · Multi-cloud</text></svg>`,
      'Cybersecurity': `<svg viewBox="0 0 240 160" fill="none"><rect width="240" height="160" fill="#fef2f2"/><path d="M120 20 L175 45 L175 95 Q175 130 120 145 Q65 130 65 95 L65 45 Z" fill="white" stroke="#fca5a5" stroke-width="2"/><path d="M120 35 L162 56 L162 95 Q162 122 120 133 Q78 122 78 95 L78 56 Z" fill="#fee2e2" opacity=".7"/><text x="106" y="100" font-size="28">🔒</text><text x="79" y="130" fill="#dc2626" font-size="10" font-weight="700" font-family="sans-serif">Security &amp; Compliance</text></svg>`
    };
    return map[cat] ?? map['Angular & .NET Core'];
  }

  ngOnInit() {
    this.api.getBlogPosts(1, 9).subscribe(r => {
      this.posts.set(r.items);
      this.total.set(r.totalCount);
    });
  }

  filterByCategory(cat: string | undefined) {
    this.activeCategory.set(cat);
    this.api.getBlogPosts(1, 9, cat).subscribe(r => {
      this.posts.set(r.items);
      this.total.set(r.totalCount);
    });
  }
}
