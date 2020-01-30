export interface TokenConfig {
  headerName: string;
  headerPrefix: string;
  tokenName: string;
  tokenGetter: any;
  noJwtError: boolean;
  globalHeaders: Array<Object>;
  noTokenScheme?: boolean;
}