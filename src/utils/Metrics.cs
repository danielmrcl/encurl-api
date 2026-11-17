namespace Encurl.Api.Utils;

using System.Diagnostics.Metrics;

public class Metrics
{
	private readonly static Meter s_link = new Meter("EncUrl.Link");

	private readonly static Counter<int> s_findsuccess = s_link.CreateCounter<int>("encurl.link.find.success");
	private readonly static Counter<int> s_finderror = s_link.CreateCounter<int>("encurl.link.find.error");
	private readonly static Counter<int> s_savesuccess = s_link.CreateCounter<int>("encurl.link.save.success");
	private readonly static Counter<int> s_saveerror = s_link.CreateCounter<int>("encurl.link.save.error");

	public static void IncLinkFind() {
		Task.Run(() => s_findsuccess.Add(1));
	}

	public static void IncLinkFindError() {
		Task.Run(() => s_finderror.Add(1));
	}

	public static void IncLinkSave() {
		Task.Run(() => s_savesuccess.Add(1));
	}

	public static void IncLinkSaveError() {
		Task.Run(() => s_saveerror.Add(1));
	}
}
