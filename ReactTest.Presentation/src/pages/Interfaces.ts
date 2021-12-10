export interface IPage {
    pageId: string;
    parentId?: string;
    pageName: string;
    pageNameEn?: string;
    orderNo?: number;
    pageYn?: boolean;
    menuYn?: boolean;
    useYn?: boolean;
}

export interface IRouteData {
    path?: string;
    exact?: boolean;
    component?: any;
}

export interface IRoutedPage extends IPage, IRouteData {
    
}