export interface BlogSummary {
  id: number;
  slug: string;
  title: string;
  excerpt: string;
  category: string;
  coverImageUrl?: string;
  authorName: string;
  readTimeMinutes: number;
  publishedAt?: string;
  viewCount: number;
}

export interface BlogDetail extends BlogSummary {
  content: string;
}
