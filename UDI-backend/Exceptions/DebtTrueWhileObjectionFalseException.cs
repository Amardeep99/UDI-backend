namespace UDI_backend.Exceptions {
	public class DebtTrueWhileObjectionFalseException : Exception {
		public DebtTrueWhileObjectionFalseException() : base("HasDebt cannot be true while HasObjection is false") { }
	}
}
