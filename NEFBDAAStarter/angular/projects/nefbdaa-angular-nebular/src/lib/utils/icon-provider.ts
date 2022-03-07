export const imagesDir = './assets/map/';
export const fileExtension = '.png';

export function getRelativeUrl(key: string) {
  switch (key && key.toLowerCase()) {
    case 'guide':
      return 'map-marker-guide';
    default:
      return 'map-marker-guest';
  }
}

export class IconProvider {
  public static getIconUrl(key: string, suffix = ''): string {
    return imagesDir + getRelativeUrl(key) + suffix + fileExtension;
  }
}
