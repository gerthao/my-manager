/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface CreateBoardRequestBody {
  name: string
  description?: null | string
}

export interface CreateBoardResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  slug: string
  description: null | string
  /** @format date-time */
  createdAt: string
}

export interface CreateColumnBody {
  name: string
}

export interface CreateColumnResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  columnId: number | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  boardSlug: string
  name: string
  /** @format date-time */
  createdAt: string
}

export interface DeleteColumnResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  columnId: number | string
  name: string
  /** @format date-time */
  modifiedAt: string
}

export interface GetBoardByIdResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  slug: string
  description: null | string
  /** @format date-time */
  createdAt: string
  /** @format date-time */
  modifiedAt: string
}

export interface GetBoardBySlugResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  slug: string
  description: null | string
  /** @format date-time */
  createdAt: string
  /** @format date-time */
  modifiedAt: string
}

export interface GetCardByIdResponse {
  name: string
  description: null | string
  /** @format date-time */
  createdAt: string
  /** @format date-time */
  modifiedAt: string
}

export interface GetColumnByIdResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  columnId: number | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  /** @format date-time */
  modifiedAt: string
}

export interface ListBoardsResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  slug: string
  /** @format date-time */
  createdAt: string
  /** @format date-time */
  modifiedAt: string
}

export interface ListCardsResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  cardId: number | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  description: null | string
  /** @format date-time */
  createdAt: string
  /** @format date-time */
  modifiedAt: string
}

export interface ListColumnsResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  columnId: number | string
  name: string
}

export interface ProblemDetails {
  type?: null | string
  title?: null | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  status?: null | number | string
  detail?: null | string
  instance?: null | string
}

export interface UpdateBoardRequestBody {
  name?: null | string
  description?: null | string
}

export interface UpdateBoardResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  name: string
  slug: string
  /** @format date-time */
  modifiedAt: string
}

export interface UpdateColumnRequestBody {
  name: null | string
}

export interface UpdateColumnResponse {
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  boardId: number | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  columnId: number | string
  name: string
  /** @format date-time */
  modifiedAt: string
}

export interface ValidationProblemDetails {
  type?: null | string
  title?: null | string
  /**
   * @format int32
   * @pattern ^-?(?:0|[1-9]\d*)$
   */
  status?: null | number | string
  detail?: null | string
  instance?: null | string
  errors?: Record<string, string[]>
}

export type QueryParamsType = Record<string | number, any>
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean
  /** request path */
  path: string
  /** content type of request body */
  type?: ContentType
  /** query params */
  query?: QueryParamsType
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat
  /** request body */
  body?: unknown
  /** base url */
  baseUrl?: string
  /** request cancellation token */
  cancelToken?: CancelToken
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<RequestParams | void> | RequestParams | void
  customFetch?: typeof fetch
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown> extends Response {
  data: D
  error: E
}

type CancelToken = Symbol | string | number

export enum ContentType {
  Json = "application/json",
  JsonApi = "application/vnd.api+json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = "http://localhost:5275"
  private securityData: SecurityDataType | null = null
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"]
  private abortControllers = new Map<CancelToken, AbortController>()
  private customFetch = (...fetchParams: Parameters<typeof fetch>) => fetch(...fetchParams)

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  }

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig)
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data
  }

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key)
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key])
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key]
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&")
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {}
    const keys = Object.keys(query).filter((key) => "undefined" !== typeof query[key])
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key),
      )
      .join("&")
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery)
    return queryString ? `?${queryString}` : ""
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.JsonApi]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.Text]: (input: any) =>
      input !== null && typeof input !== "string" ? JSON.stringify(input) : input,
    [ContentType.FormData]: (input: any) => {
      if (input instanceof FormData) {
        return input
      }

      return Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key]
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
              ? JSON.stringify(property)
              : `${property}`,
        )
        return formData
      }, new FormData())
    },
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  }

  protected mergeRequestParams(params1: RequestParams, params2?: RequestParams): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    }
  }

  protected createAbortSignal = (cancelToken: CancelToken): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken)
      if (abortController) {
        return abortController.signal
      }
      return void 0
    }

    const abortController = new AbortController()
    this.abortControllers.set(cancelToken, abortController)
    return abortController.signal
  }

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken)

    if (abortController) {
      abortController.abort()
      this.abortControllers.delete(cancelToken)
    }
  }

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {}
    const requestParams = this.mergeRequestParams(params, secureParams)
    const queryString = query && this.toQueryString(query)
    const payloadFormatter = this.contentFormatters[type || ContentType.Json]
    const responseFormat = format || requestParams.format

    return this.customFetch(
      `${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData ? { "Content-Type": type } : {}),
        },
        signal: (cancelToken ? this.createAbortSignal(cancelToken) : requestParams.signal) || null,
        body: typeof body === "undefined" || body === null ? null : payloadFormatter(body),
      },
    ).then(async (response) => {
      const r = response as HttpResponse<T, E>
      r.data = null as unknown as T
      r.error = null as unknown as E

      const responseToParse = responseFormat ? response.clone() : response
      const data = !responseFormat
        ? r
        : await responseToParse[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data
              } else {
                r.error = data
              }
              return r
            })
            .catch((e) => {
              r.error = e
              return r
            })

      if (cancelToken) {
        this.abortControllers.delete(cancelToken)
      }

      return data
    })
  }
}

/**
 * @title MyManager | v1
 * @version 1.0.0
 * @baseUrl http://localhost:5275
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  home = {
    /**
     * No description
     *
     * @tags HomeEndpoint
     * @name HomeList
     * @request GET:/home
     */
    homeList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/home`,
        method: "GET",
        ...params,
      }),
  }
  api = {
    /**
     * No description
     *
     * @tags Boards
     * @name GetBoardById
     * @summary Get a board by ID
     * @request GET:/api/project-board/boards/{boardId}
     */
    getBoardById: (boardId: number | string, params: RequestParams = {}) =>
      this.request<GetBoardByIdResponse, ProblemDetails>({
        path: `/api/project-board/boards/${boardId}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Boards
     * @name DeleteBoard
     * @summary Deletes an existing board
     * @request DELETE:/api/project-board/boards/{boardId}
     */
    deleteBoard: (boardId: number | string, params: RequestParams = {}) =>
      this.request<void, ProblemDetails>({
        path: `/api/project-board/boards/${boardId}`,
        method: "DELETE",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Boards
     * @name UpdateBoard
     * @summary Updates an existing board
     * @request PATCH:/api/project-board/boards/{boardId}
     */
    updateBoard: (
      boardId: number | string,
      data: UpdateBoardRequestBody,
      params: RequestParams = {},
    ) =>
      this.request<UpdateBoardResponse, ValidationProblemDetails | ProblemDetails>({
        path: `/api/project-board/boards/${boardId}`,
        method: "PATCH",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Boards
     * @name GetBoardBySlug
     * @summary Gets a board by slug and ID
     * @request GET:/api/project-board/boards/{boardId}-{slug}
     */
    getBoardBySlug: (boardId: number | string, slug: string, params: RequestParams = {}) =>
      this.request<GetBoardBySlugResponse, ProblemDetails>({
        path: `/api/project-board/boards/${boardId}-${slug}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Boards
     * @name ListBoards
     * @summary List boards
     * @request GET:/api/project-board/boards
     */
    listBoards: (
      query?: {
        Name?: string
      },
      params: RequestParams = {},
    ) =>
      this.request<ListBoardsResponse[], any>({
        path: `/api/project-board/boards`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Boards
     * @name CreateBoard
     * @summary Creates a new board
     * @request POST:/api/project-board/boards
     */
    createBoard: (data: CreateBoardRequestBody, params: RequestParams = {}) =>
      this.request<CreateBoardResponse, ValidationProblemDetails | ProblemDetails>({
        path: `/api/project-board/boards`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Cards
     * @name GetCard
     * @summary Get a card from a board by ID
     * @request GET:/api/project-board/boards/{boardId}/cards/{cardId}
     */
    getCard: (boardId: number | string, cardId: number | string, params: RequestParams = {}) =>
      this.request<GetCardByIdResponse, ProblemDetails>({
        path: `/api/project-board/boards/${boardId}/cards/${cardId}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Cards
     * @name ListCards
     * @summary List cards of a board
     * @request GET:/api/project-board/{boardId}/cards
     */
    listCards: (boardId: number | string, params: RequestParams = {}) =>
      this.request<ListCardsResponse[], any>({
        path: `/api/project-board/${boardId}/cards`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Columns
     * @name GetColumnById
     * @summary Get a column by its ID
     * @request GET:/api/project-board/boards/{boardId}/columns/{columnId}
     */
    getColumnById: (
      boardId: number | string,
      columnId: number | string,
      params: RequestParams = {},
    ) =>
      this.request<GetColumnByIdResponse, ProblemDetails>({
        path: `/api/project-board/boards/${boardId}/columns/${columnId}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Columns
     * @name DeleteColumn
     * @summary Delete a column from its board
     * @request DELETE:/api/project-board/boards/{boardId}/columns/{columnId}
     */
    deleteColumn: (
      boardId: number | string,
      columnId: number | string,
      params: RequestParams = {},
    ) =>
      this.request<DeleteColumnResponse, void>({
        path: `/api/project-board/boards/${boardId}/columns/${columnId}`,
        method: "DELETE",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Columns
     * @name UpdateColumn
     * @summary Updates an existing column for a board
     * @request PATCH:/api/project-board/boards/{boardId}/columns/{columnId}
     */
    updateColumn: (
      boardId: number | string,
      columnId: number | string,
      data: UpdateColumnRequestBody,
      params: RequestParams = {},
    ) =>
      this.request<UpdateColumnResponse, ValidationProblemDetails | ProblemDetails>({
        path: `/api/project-board/boards/${boardId}/columns/${columnId}`,
        method: "PATCH",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Columns
     * @name ListColumns
     * @summary List columns for a board
     * @request GET:/api/project-board/boards/{boardId}/columns
     */
    listColumns: (boardId: number | string, params: RequestParams = {}) =>
      this.request<ListColumnsResponse[], any>({
        path: `/api/project-board/boards/${boardId}/columns`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Columns
     * @name CreateColumn
     * @summary Creates a new column for a project board.
     * @request POST:/api/project-board/boards/{boardId}/columns
     */
    createColumn: (boardId: number | string, data: CreateColumnBody, params: RequestParams = {}) =>
      this.request<CreateColumnResponse, ValidationProblemDetails | ProblemDetails>({
        path: `/api/project-board/boards/${boardId}/columns`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
}
