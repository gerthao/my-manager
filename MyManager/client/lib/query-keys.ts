// Auto-generated query keys
// Central place to define all query keys

export const queryKeys = {
  // HomeEndpoint
  homeEndpoint: {
    all: ["homeEndpoint"] as const,
    lists: () => [...queryKeys.homeEndpoint.all, "list"] as const,
    list: (filters?: any) => [...queryKeys.homeEndpoint.lists(), filters] as const,
    details: () => [...queryKeys.homeEndpoint.all, "detail"] as const,
    detail: (id: string) => [...queryKeys.homeEndpoint.details(), id] as const,
  },
  // Boards
  boards: {
    all: ["boards"] as const,
    lists: () => [...queryKeys.boards.all, "list"] as const,
    list: (filters?: any) => [...queryKeys.boards.lists(), filters] as const,
    details: () => [...queryKeys.boards.all, "detail"] as const,
    detail: (id: string) => [...queryKeys.boards.details(), id] as const,
  },
  // Cards
  cards: {
    all: ["cards"] as const,
    lists: () => [...queryKeys.cards.all, "list"] as const,
    list: (filters?: any) => [...queryKeys.cards.lists(), filters] as const,
    details: () => [...queryKeys.cards.all, "detail"] as const,
    detail: (id: string) => [...queryKeys.cards.details(), id] as const,
  },
  // Columns
  columns: {
    all: ["columns"] as const,
    lists: () => [...queryKeys.columns.all, "list"] as const,
    list: (filters?: any) => [...queryKeys.columns.lists(), filters] as const,
    details: () => [...queryKeys.columns.all, "detail"] as const,
    detail: (id: string) => [...queryKeys.columns.details(), id] as const,
  },
};
