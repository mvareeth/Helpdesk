import { Inject } from '@angular/core';

export class BaseService {
  public totalCount: number = 0;
  private baseUri: string;
  private pageSize: number;

  public constructor(@Inject(String) baseUri: string, @Inject(Number) pageSize: number,
    @Inject(Number) page: number, @Inject(Number) pagesCount: number) {
    this.baseUri = baseUri;
    this.pageSize = pageSize;
    this.totalCount = pagesCount * pageSize;
  }

  public getURL = (actionUrl = '', page = 0): string => {
    let url = this.baseUri;
    if (actionUrl !== '') {
      url += '/' + actionUrl;
    }
    if (page > 0) {
      url += '/' + page.toString() + '/' + this.pageSize.toString();
    }
    return url;
  }

  public set setBaseUri(uri: string) {
    this.baseUri = uri;
  }
}
