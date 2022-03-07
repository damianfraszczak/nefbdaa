import {Injectable} from '@angular/core';
import {constants} from '../shared-constants';

export interface ICacheElement<T> {
  key: string;
  val: T;
  lastRead: number;
}


@Injectable({
  providedIn: 'root',
})
export class CacheService {
  private masterCacheKey = constants.cacheKey;
  private maxAge = constants.cacheMaxAge;
  private localCache: Map<string, ICacheElement<any>> = new Map();

  constructor() {
    if (typeof localStorage === 'undefined') {
      throw new Error('Platform not supported.');
    }
  }

  add(cacheKey: string, val: any) {
    this.addToMapOrLocalStorage({val: val, lastRead: Date.now(), key: cacheKey}, cacheKey);
  }

  get(cacheKey: string): ICacheElement<any> {
    return this.getFromMapOrLocalStorage()[cacheKey];
  }

  getAll() {
    return this.getFromMapOrLocalStorage();
  }

  remove(cacheKey: string) {
    this.removeFromMapOrLocalStorage(cacheKey);
  }

  /*removeExpiredEntries() {
    const expired = Date.now() - this.maxAge;
    const cache = this.getAll();

    cache.forEach(entry => {
      if (entry.lastRead < expired) {
        this.remove(entry.key);
      }
    });
  }*/


  private removeFromMapOrLocalStorage(cacheKey: string) {
    if (this.localStorageExists()) {
      const allCachedData = this.getRawData();
      allCachedData[cacheKey] = null;
      this.storeRawData(allCachedData);
    } else {
      this.localCache.delete(cacheKey);
    }

  }

  private localStorageExists(): boolean {
    return !(typeof localStorage === 'undefined');
  }

  private getFromMapOrLocalStorage() {
    if (this.localStorageExists()) {
      return this.getRawData();
    } else {
      return this.localCache.values;
    }
  }

  private getRawData(): { [key: string]: ICacheElement<any> } {
    const data = localStorage.getItem(this.masterCacheKey);
    try {
      return JSON.parse(data) || {};
    } catch (error) {
      throw new Error(error);
    }
  }

  private storeRawData(data: { [key: string]: ICacheElement<any> }): void {
    localStorage.setItem(this.masterCacheKey, JSON.stringify(data));
  }


  private addToMapOrLocalStorage(cacheElement: ICacheElement<any>, cacheKey: string) {
    if (this.localStorageExists()) {
      const allCachedData = this.getRawData();
      allCachedData[cacheKey] = cacheElement;
      this.storeRawData(allCachedData);
    } else {
      this.localCache[cacheKey] = cacheElement;
    }
  }
}
